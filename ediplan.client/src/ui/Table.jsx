import { createContext, useContext } from "react";
import styled from "styled-components";
import { getAssetVariableName, getBookingVariableName } from "../utils/helpers";

// import Empty from "./Empty";

const StyledTable = styled.div`
  border: 1px solid var(--color-grey-200);

  font-size: 1.4rem;
  background-color: var(--color-grey-0);
  border-radius: 7px;
  overflow: hidden;
`;

const CommonRow = styled.div`
  display: grid;
  grid-template-columns: ${(props) => props.$columnWidths};
  column-gap: 1.8rem;
  align-items: center;
  transition: none;
`;

const StyledHeader = styled(CommonRow)`
  padding: 1.6rem 2.4rem;

  background-color: var(--color-grey-50);
  border-bottom: 1px solid var(--color-grey-100);
  text-transform: uppercase;
  letter-spacing: 0.4px;
  font-weight: 600;
  color: var(--color-grey-600);
`;

const StyledBody = styled.section`
  margin: 0.4rem 0;
`;

const StyledRow = styled(CommonRow)`
  padding: 0.5rem 2.4rem;

  &:not(:last-child) {
    border-bottom: 1px solid var(--color-grey-100);
  }
`;

const CommonItem = styled.div`
  border-right: 1px solid var(--color-grey-200);
  overflow-wrap: anywhere;
`;

const StyledRowItem = styled(CommonItem)``;

const StyledHeaderItem = styled(CommonItem)`
  display: flex;
  justify-content: space-around;
`;

const Footer = styled.footer`
  background-color: var(--color-grey-50);
  display: flex;
  justify-content: center;
  padding: 1.2rem;

  &:not(:has(*)) {
    display: none;
  }
`;

const Empty = styled.p`
  font-size: 1.6rem;
  font-weight: 500;
  text-align: center;
  margin: 2.4rem;
`;

const TableContext = createContext();

function Table({ displayColumns, columnWidths, rowData, children }) {
  return (
    <TableContext.Provider value={{ displayColumns, columnWidths }}>
      <StyledTable>{children}</StyledTable>
    </TableContext.Provider>
  );
}

function Header({ render }) {
  const { displayColumns, columnWidths } = useContext(TableContext);

  return (
    <StyledHeader role="row" $columnWidths={columnWidths} as="header">
      {displayColumns.map(render)}
    </StyledHeader>
  );
}

function Row({ data, render, children }) {
  const { displayColumns, columnWidths } = useContext(TableContext);
  const columns = displayColumns.map((column) =>
    getBookingVariableName(column)
  );
  console.log(`Chosen values: `, columns);
  console.log("Table.Row before reduce: ", data);

  const filteredVals = Object.keys(data).reduce((acc, key) => {
    if (columns.includes(key)) {
      acc.push(data[key]);
    }
    return acc;
  }, []);

  console.log("Table.Row after reduce: ", filteredVals);

  const items = filteredVals.map(render);

  return (
    <StyledRow role="row" $columnWidths={columnWidths}>
      {items}
      {children}
    </StyledRow>
  );
}

function HeaderItem({ children }) {
  return <StyledHeaderItem>{children}</StyledHeaderItem>;
}

function Item({ children }) {
  return <StyledRowItem>{children}</StyledRowItem>;
}

function Body({ data, render }) {

  if (!data.length) return <Empty>No data to display</Empty>;
  return <StyledBody>{data.map(render)}</StyledBody>;
}

Table.Header = Header;
Table.Footer = Footer;
Table.Row = Row;
Table.Item = Item;
Table.HeaderItem = HeaderItem;
Table.Body = Body;

export default Table;

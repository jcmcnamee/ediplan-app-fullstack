import { flexRender } from '@tanstack/react-table';
import styled from 'styled-components';

const StyledTable = styled.table`
  outline: 1px solid var(--color-grey-200);

  font-size: 1.4rem;
  background-color: var(--color-grey-0);
  border-radius: 7px;
  overflow: hidden;
  border-collapse: collapse;
`;

const StyledTableBody = styled.tbody`
  & td {
    border: 1px solid var(--color-grey-100);
    padding: 0rem 1rem;
  }
`;

const CommonRow = styled.tr``;

const StyledHeaderRow = styled(CommonRow)`
  padding: 1.6rem 2.4rem;
  background-color: var(--color-grey-50);
  border-bottom: 1px solid var(--color-grey-300);
  text-transform: uppercase;
  letter-spacing: 0.4px;
  font-weight: 600;
  color: var(--color-grey-600);
`;

const CommonItem = styled.div`
  overflow-wrap: anywhere;
`;

const StyledHeaderItem = styled(CommonItem)`
  padding: 1rem 0.1rem;
  display: flex;
  justify-content: space-around;
`;

function MiniTableLayout({ table }) {
  const headerGroups = table.getHeaderGroups();
  const rowModel = table.getRowModel();
  // const columns = table.getAllColums();

  return (
    <StyledTable>
      <thead>
        {headerGroups.map(headerGroup => (
          <StyledHeaderRow key={headerGroup.id}>
            {headerGroup.headers.map(headerItem => (
              <th key={headerItem.id}>
                <StyledHeaderItem>
                  {headerItem.isPlaceholder
                    ? null
                    : flexRender(
                        headerItem.column.columnDef.header,
                        headerItem.getContext()
                      )}
                </StyledHeaderItem>
              </th>
            ))}
          </StyledHeaderRow>
        ))}
      </thead>
      <StyledTableBody>
        {rowModel.rows.map(row => (
          <tr key={row.id}>
            {row.getVisibleCells().map(cell => (
              <td key={cell.id}>
                {flexRender(cell.column.columnDef.cell, cell.getContext())}
              </td>
            ))}
          </tr>
        ))}
      </StyledTableBody>
    </StyledTable>
  );
}

export default MiniTableLayout;

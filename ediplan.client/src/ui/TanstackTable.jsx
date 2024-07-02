import { createContext, memo, useContext, useMemo, useState } from 'react';
import styled from 'styled-components';
import { getAssetVariableName, getBookingVariableName } from '../utils/helpers';
import {
  flexRender,
  getCoreRowModel,
  getFacetedMinMaxValues,
  getFacetedUniqueValues,
  useReactTable,
} from '@tanstack/react-table';
import { useSearchParams } from 'react-router-dom';
import Filter from './Filter';

// #region Old styles

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

const StyledBody = styled.section`
  margin: 0.4rem 0;
`;

const StyledRow = styled(CommonRow)`
  padding: 0.5rem 2.4rem;

  &:not(:last-child) {
    border: 1px solid var(--color-grey-100);
  }
`;

const CommonItem = styled.div`
  overflow-wrap: anywhere;
`;

const StyledRowItem = styled(CommonItem)``;

const StyledHeaderItem = styled(CommonItem)`
  padding: 1rem 0.1rem;
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

// #endregion

const TableContext = createContext();

function TanstackTable({ data, columnDefinitions, children }) {
  const [pagination, setPagination] = useState({ pageIndex: 0, pageSize: 5 });

  const table = useReactTable({
    data: data ?? [],
    columns: columnDefinitions,
    manualPagination: true,
    rowCount: data?.rowCount,
    state: {
      pagination,
    },
    onPaginationChange: setPagination,
    initialState: {
      columnVisibility: {
        created: false,
        modified: false,
      },
    },
    getCoreRowModel: getCoreRowModel(),
    getFacetedMinMaxValues: getFacetedMinMaxValues(),
    getFacetedUniqueValues: getFacetedUniqueValues(),
  });

  const headerGroups = table.getHeaderGroups();
  const rowModel = table.getRowModel();
  const columns = table.getAllColumns();
  // const canGetPrevPage = table.getCanPreviousPage;
  // const setPageIndex = table.setPageIndex;
  // const canGetNextPage = table.canGetNextPage;
  // const pageSize = table.getState.pageSize;
  // const tableState = table.getState();

  return (
    <TableContext.Provider
      value={{
        headerGroups,
        rowModel,
        columns,
      }}>
      {children}
    </TableContext.Provider>
  );
}

function TableWrapper({ children }) {
  return <StyledTable>{children}</StyledTable>;
}

function Header({ children }) {
  const { headerGroups } = useContext(TableContext);

  return (
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
  );
}

function Body() {
  const { rowModel } = useContext(TableContext);

  return (
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
  );
}

function FilterMenu() {
  const [searchParams, setSearchParams] = useSearchParams();
  const { columns } = useContext(TableContext);

  return (
    // <div>
    //   Headers:{" "}
    //   {columns.map((column) => (
    //     <div key={column.id}>
    //       <strong>{column.columnDef.header} MinMax Values:</strong> {column.getFacetedMinMaxValues()} Filter: <Filter column={column} table={table} />
    //     </div>
    //   ))}

    // </div>
    'hello'
  );
}

// function Pagination() {
//   const {
//     getCanPreviousPage,
//     getCanNextPage,
//     setPageIndex,
//     previousPage,
//     getPageCount,
//     pageSize,
//     setPageSize,
//     nextPage,
//     tableState,
//   } = useContext(TableContext);
//   return (
//     <footer className="pagination">
//       <button disabled={!getCanPreviousPage} onClick={() => setPageIndex(0)}>
//         ⏪
//       </button>
//       <button disabled={!getCanPreviousPage} onClick={previousPage}>
//         ◀️
//       </button>
//       <span>{`page ${
//         tableState.pagination.pageIndex + 1
//       } of ${getPageCount()}`}</span>
//       <button disabled={getCanNextPage} onClick={nextPage}>
//         ▶️
//       </button>
//       <button
//         disabled={!getCanNextPage()}
//         onClick={() => setPageIndex(getPageCount() - 1)}>
//         ⏩
//       </button>
//       <span>Show: </span>
//       <select
//         value={pageSize}
//         onChange={e => setPageSize(parseInt(e.target.value, 10))}>
//         {[5, 10, 20].map(size => (
//           <option key={size} value={size}>
//             {size}
//           </option>
//         ))}
//       </select>
//       <span> items per page</span>
//     </footer>
//   );
// }

TanstackTable.Wrapper = TableWrapper;
TanstackTable.Header = Header;
TanstackTable.Body = Body;
TanstackTable.FilterMenu = FilterMenu;
// TanstackTable.Pagination = Pagination;

export default TanstackTable;

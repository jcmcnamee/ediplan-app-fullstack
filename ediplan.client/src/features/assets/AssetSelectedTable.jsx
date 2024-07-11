import {
  createColumnHelper,
  getCoreRowModel,
  getFacetedMinMaxValues,
  getFacetedUniqueValues,
  useReactTable
} from '@tanstack/react-table';
import { useMemo, useState } from 'react';
import MiniTableLayout from '../../ui/Table/MiniTableLayout';
// import IndeterminateCheckbox from '../../ui/Form/IndeterminateCheckbox';
import { LuChevronRight } from 'react-icons/lu';

function AssetSelectedTable({ tableData, rowSelection, setRowSelection, toggleRowSelection }) {
  // const [pagination, setPagination] = useState(null);

  const [columns, data] = useMemo(() => {
    const column = createColumnHelper();

    const columns = [
      column.accessor('id', {
        header: 'ID'
      }),
      column.accessor('type', {
        header: 'type'
      }),
      column.accessor('name', {
        header: 'Name'
      }),
      column.display({
        id: 'Select',
        cell: ({ row }) => (
          // <div className="px-1">
          //   <IndeterminateCheckbox
          //     {...{
          //       checked: row.getIsSelected(),
          //       disabled: !row.getCanSelect(),
          //       indeterminate: row.getIsSomeSelected(),
          //       onChange: row.getToggleSelectedHandler(),
          //     }}
          //   />
          // </div>
          <button onClick={() => toggleRowSelection(row.id)}>
            <LuChevronRight />
          </button>
        )
      })
    ];

    return [columns, tableData];
  }, [tableData]);

  const table = useReactTable({
    data: data ?? [],
    columns: columns,
    manualPagination: true,
    state: {
      // pagination,
      rowSelection
    },
    // onPaginationChange: setPagination,
    onRowSelectionChange: setRowSelection,
    initialState: {
      columnVisibility: {
        created: false,
        modified: false
      }
    },
    getCoreRowModel: getCoreRowModel(),
    getFacetedMinMaxValues: getFacetedMinMaxValues(),
    getFacetedUniqueValues: getFacetedUniqueValues(),
    getRowId: row => row.id,
    enableRowSelection: true,
    enableMultiRowSelection: false
  });

  return <MiniTableLayout table={table} />;
}

export default AssetSelectedTable;

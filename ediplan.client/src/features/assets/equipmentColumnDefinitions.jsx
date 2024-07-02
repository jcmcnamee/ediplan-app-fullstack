import { createColumnHelper } from '@tanstack/react-table';

const columnHelper = createColumnHelper();

export const equipmentColumnDefinitions = [
  columnHelper.accessor('assetNumber', {
    header: 'Tag',
  }),
  columnHelper.accessor('name', {
    header: 'Name',
  }),
  columnHelper.accessor('make', {
    Header: 'Make',
  }),
  columnHelper.accessor('model', {
    header: 'Model',
  }),
  columnHelper.accessor('value', {
    header: 'Value',
  }),
  columnHelper.accessor('rate', {
    header: 'Rate',
  }),
  columnHelper.accessor('rateUnit', {
    header: 'Currency',
  }),
  columnHelper.accessor('description', {
    header: 'Description',
  }),
];

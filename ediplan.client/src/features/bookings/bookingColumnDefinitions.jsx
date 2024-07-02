import { createColumnHelper } from '@tanstack/react-table';
import { format, parse } from 'date-fns';
import StackedCell from '../../ui/StackedCell';
import BookingTableMenu from './BookingTableMenu';

const columnHelper = createColumnHelper();

export const bookingColumnDefinitions = [
  columnHelper.accessor('id', {
    header: 'ID',
  }),
  columnHelper.accessor('name', {
    header: 'Name',
  }),
  columnHelper.accessor('startDate', {
    header: 'Start',
    cell: props => {
      return (
        <StackedCell
          data1={format(
            parse(
              props.getValue(),
              "yyyy-MM-dd'T'HH:mm:ss.SSSSSSxxx",
              new Date()
            ),
            'E do MMM yyyy'
          )}
          data2="Secondary data"
        />
      );
    },
  }),
  columnHelper.accessor('endDate', {
    header: 'End',
    cell: props => {
      return format(
        parse(props.getValue(), "yyyy-MM-dd'T'HH:mm:ss.SSSSSSxxx", new Date()),
        'E do MMM yyyy'
      );
    },
  }),
  columnHelper.accessor('productionName', {
    header: 'Production',
  }),
  columnHelper.accessor('locationName', {
    header: 'Location',
  }),
  columnHelper.accessor('status', {
    header: 'Status',
  }),
  columnHelper.accessor('created', {
    header: 'Created',
    cell: props => {
      return format(
        parse(props.getValue(), "yyyy-MM-dd'T'HH:mm:ss.SSSSSSxxx", new Date()),
        'E do MMM yyyy'
      );
    },
  }),
  columnHelper.accessor('modified', {
    header: 'Modified',
    cell: props => {
      return format(
        parse(props.getValue(), "yyyy-MM-dd'T'HH:mm:ss.SSSSSSxxx", new Date()),
        'E do MMM yyyy'
      );
    },
  }),
  columnHelper.accessor('notes', {
    header: 'Notes',
  }),
  columnHelper.display({
    header: 'Menus',
    cell: props => <BookingTableMenu assetId={props.row.original.id} />,
  }),
];

import Table from "../../ui/Table";
import RowItem from "../assets/RowItem";

function BookingRow({ booking: {
  id: bookingId,
  created, startDate, endDate
} }) {
  return (
    <Table.Row role="row">
      <div>Menu</div>
    </Table.Row>
  );
}

export default BookingRow;

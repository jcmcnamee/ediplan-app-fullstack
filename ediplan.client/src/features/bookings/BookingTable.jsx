import styled from 'styled-components';

import { useBookings } from './useBookings';

import Spinner from '../../ui/Spinner';
import Menus from '../../ui/Menus';
import Empty from '../../ui/Empty';
import TanstackTable from '../../ui/TanstackTable';

import { bookingColumnDefinitions } from './bookingColumnDefinitions';
import { useState } from 'react';

const Container = styled.div`
  display: flex;
  flex-direction: column;
  gap: 20px;
`;

function BookingTable() {
  const [isFilterOpen, setIsFilterOpen] = useState(false);

  const { bookings, error, isPending } = useBookings();

  if (error) return <div>{error}</div>;
  if (isPending) return <Spinner />;

  const data = bookings;

  if (!data.length) return <Empty resource="bookings" />;

  return (
    <Container>
      <Menus>
        <TanstackTable
          data={bookings}
          columnDefinitions={bookingColumnDefinitions}>
          <TanstackTable.Wrapper>
            <TanstackTable.Header />
            <TanstackTable.Body />
          </TanstackTable.Wrapper>
          {/* <TanstackTable.Pagination /> */}
        </TanstackTable>
      </Menus>
    </Container>
  );
}

export default BookingTable;

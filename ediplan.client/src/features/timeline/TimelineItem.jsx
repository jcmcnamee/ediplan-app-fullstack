import styled from 'styled-components';
import { differenceInDays } from 'date-fns';
import { useTimeline } from './TimelineContext';

const StyledTimelineItem = styled.div`
  background-color: darkgoldenrod;
  grid-column: ${props => props.$startUnit} / span 20;
`;

function TimelineItem({ booking }) {
  const { viewDate } = useTimeline();
  const startUnit = differenceInDays(booking.startDate, viewDate);

  const bookingInfo = new Date(booking.startDate).toDateString();

  return (
    <StyledTimelineItem $startUnit={startUnit}>
      {bookingInfo}
    </StyledTimelineItem>
  );
}

export default TimelineItem;

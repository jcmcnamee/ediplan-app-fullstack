import styled from 'styled-components';
import { useTimeline } from './TimelineContext';

const StyledTimelineTrack = styled.div`
  background-color: lightcoral;
  border: 1px solid var(--color-grey-200);
  width: fit-content;

  display: grid;
  grid-template-rows: 8rem;
  /* grid-template-columns: ${props => `repeat(${props.timeUnits}, 20px)`}; */
  grid-template-columns: ${props => `repeat(${props.$numUnits}, ${props.$unitWidth}rem)`};
`;

function TimelineTrack({ children }) {
  const { startDate, numUnits, unitWidth } = useTimeline();

  return (
    <StyledTimelineTrack $numUnits={numUnits} $unitWidth={unitWidth}>
      TimelineTrack{children}
    </StyledTimelineTrack>
  );
}

export default TimelineTrack;

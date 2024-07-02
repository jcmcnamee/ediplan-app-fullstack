import styled from 'styled-components';
import TimelineRuler from './TimelineRuler';
import TimelineTrack from './TimelineTrack';
import TimelineItem from './TimelineItem';

const StyledTimelineTrackContainer = styled.div`
  grid-area: 2 / 2;
  overflow-x: auto;
  overflow-y: auto;
  border: 1px dotted var(--color-grey-400);
`;

function TimelineTrackContainer({ booking }) {
  return (
    <StyledTimelineTrackContainer>
      TimelineTrackContainer
      <TimelineRuler

      />
      <TimelineTrack >
        <TimelineItem booking={booking}></TimelineItem>
      </TimelineTrack>
    </StyledTimelineTrackContainer>
  );
}

export default TimelineTrackContainer;

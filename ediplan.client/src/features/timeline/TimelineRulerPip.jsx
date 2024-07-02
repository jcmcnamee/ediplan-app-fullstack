import styled from 'styled-components';

const StyledTimelineRulerPip = styled.div`
  border-left: 2px solid var(--color-grey-500);

  height: 0.75rem;
  grid-column: ${props => props.$date};
`;

function TimelineRulerPip({ date }) {
  return <StyledTimelineRulerPip $date={date}></StyledTimelineRulerPip>;
}

export default TimelineRulerPip;

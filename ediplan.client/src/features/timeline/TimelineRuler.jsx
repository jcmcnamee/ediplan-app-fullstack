import { useMemo } from 'react';
import { differenceInDays, eachMonthOfInterval } from 'date-fns';
import styled, { css } from 'styled-components';
import { useTimeline } from './TimelineContext';
import TimelineRulerPip from './TimelineRulerPip';

const StyledTimelineRuler = styled.div`
  background-color: var(--color-brand-100);
  width: fit-content;
  height: 2rem;
  border-bottom: 1px solid var(--color-grey-400);

  grid-area: 1 / 2;

  display: grid;
  ${props =>
    `grid-template-columns: repeat(${props.$numUnits}, ${props.$unitWidth}rem)`};
  align-items: end;
`;

function TimelineRuler() {
  const { startDate, endDate, numUnits, unitWidth } = useTimeline();

  // Get all of the months
  const months = useMemo(() => {
    return eachMonthOfInterval({
      start: startDate,
      end: endDate,
    }).slice(1);
  }, [startDate, endDate]);

  return (
    <StyledTimelineRuler $numUnits={numUnits} $unitWidth={unitWidth}>
      {months.map(pip => (
        <TimelineRulerPip date={differenceInDays(pip, startDate)} key={pip} />
      ))}
    </StyledTimelineRuler>
  );
}

export default TimelineRuler;

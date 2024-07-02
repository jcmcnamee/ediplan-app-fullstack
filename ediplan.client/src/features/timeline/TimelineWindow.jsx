import { useState } from 'react';
import { addYears, differenceInDays, subDays } from 'date-fns';

import styled from 'styled-components';
import TimelineTrackContainer from './TimelineTrackContainer';
import Toolbar from '../../ui/Toolbar';
import { useTimeline } from './TimelineContext';

const StyledContainer = styled.div`
  height: 100%;
  width: auto;
  background-color: var(--color-brand-50);

  display: grid;
  grid-template-columns: 23rem 1fr;
  grid-template-rows: 3rem 1fr;
`;

function TimelineWindow({ booking, currentDate }) {
  // Local state
  // const [viewDate, setViewDate] = useState(currentDate);
  // const [units, setUnits] = useState('days');
  // const [unitWidth, setUnitWidth] = useState(2);
  // const [startDate, setStartDate] = useState(currentDate);
  // const [endDate, setEndDate] = useState(subDays(addYears(currentDate, 1), 1));

  // // Derived state
  // const numUnits = differenceInDays(endDate, startDate) - 1;
  const { unitWidth, dispatch } = useTimeline();

  function handleIncrement() {
    dispatch({ type: 'incUnitWidth', payload: 0.1 });
  }
  function handleDecrement() {
    dispatch({ type: 'decUnitWidth', payload: 0.1 });
  }

  return (
    <StyledContainer>
      TimelineWindow
      {/* <Toolbar>
        <ToolbarButton
          $size="single"
          $variation="primary"
          onClick={handleDecrement}
        >
          <span>-</span>
        </ToolbarButton>
        <ToolbarButton
          $size="single"
          $variation="primary"
          onClick={handleIncrement}
        >
          <span>+</span>
        </ToolbarButton>
        <span>{unitWidth}</span>
      </Toolbar> */}
      <Toolbar>
        <Toolbar.Panel side="left">
          <Toolbar.Button $variation="secondary" onClick={handleDecrement}>
            <span>-</span>
          </Toolbar.Button>
        </Toolbar.Panel>
        <Toolbar.Panel side="right">
          <Toolbar.Button $variation="secondary" onClick={handleIncrement}>
            <span>+</span>
          </Toolbar.Button>
        </Toolbar.Panel>
      </Toolbar>
      <TimelineTrackContainer booking={booking} />
    </StyledContainer>
  );
}

export default TimelineWindow;

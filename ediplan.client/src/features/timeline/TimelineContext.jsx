import { useContext, useReducer, createContext } from 'react';
import { addYears, differenceInDays, subDays } from 'date-fns';

const TimelineContext = createContext();

function reducer(state, action) {
  switch (action.type) {
    case 'setViewDate':
      return {
        ...state,
        viewDate: action.payload,
      };
    case 'setUnits':
      return {
        ...state,
        units: action.payload,
      };
    case 'setUnitWidth':
      return {
        ...state,
        unitWidth: action.payload,
      };
    case 'incUnitWidth':
      return {
        ...state,
        unitWidth: state.unitWidth + action.payload,
      };
    case 'decUnitWidth':
      return {
        ...state,
        unitWidth: state.unitWidth - action.payload,
      };
    case 'setStartDate':
      return {
        ...state,
        startDate: action.payload,
      };
    case 'setEndDate':
      return {
        ...state,
        endDate: action.payload,
      };
    default:
      return state;
  }
}

const initialState = {
  viewDate: new Date(),
  units: 'month',
  unitWidth: 2,
  startDate: new Date(),
  endDate: subDays(addYears(new Date(), 1), 1),
};

function TimelineProvider({ children }) {
  const [{ viewDate, units, unitWidth, startDate, endDate }, dispatch] =
    useReducer(reducer, initialState);

  // user any hooks or effects here
  const numUnits = differenceInDays(endDate, startDate) - 1;

  return (
    <TimelineContext.Provider
      value={{
        viewDate,
        units,
        unitWidth,
        startDate,
        endDate,
        numUnits,
        dispatch,
      }}
    >
      {children}
    </TimelineContext.Provider>
  );
}

function useTimeline() {
  const context = useContext(TimelineContext);
  if (context === undefined)
    return new Error('TimelineContext was used outside of CitiesProvider');
  return context; // Return value
}

export { TimelineProvider, useTimeline };

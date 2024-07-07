import { addDays, formatISO } from 'date-fns';
import { useReducer } from 'react';

function useAssetFilters() {
  const initialState = {
    type: null,
    from: null,
    to: null,
    sortBy: 'createdDate desc',
    search: null,
    page: 1,
    pageSize: 10,
  };
  // function useAssetFilters() {
  //   const initialState = {
  //     type: 'all',
  //     from: formatISO(new Date()),
  //     to: formatISO(addDays(new Date(), 7)),
  //     sortBy: 'createdDate desc',
  //     search: 'Edit',
  //   };

  const reducer = (state, action) => {
    switch (action.type) {
      case 'filterType':
        return {
          ...state,
          type: action.payload,
        };
      case 'filterFromDate':
        return {
          ...state,
          from: action.payload,
        };
      case 'filterToDate':
        return {
          ...state,
          to: action.payload,
        };
      case 'search':
        return {
          ...state,
          search: action.payload,
        };
      case 'nextPage':
        return {
          ...state,
          page: state.page + 1,
        };
      case 'prevPage':
        return {
          ...state,
          page: state.page - 1,
        };
      default:
        throw new Error('Unknown action type: ', action.type);
    }
  };

  const [state, dispatch] = useReducer(reducer, initialState);

  console.log('Asset filter state update: ', state);

  return { state, dispatch };
}

export default useAssetFilters;

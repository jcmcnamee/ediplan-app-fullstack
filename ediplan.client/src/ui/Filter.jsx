import styled, { css } from 'styled-components';
import { useSearchParams } from 'react-router-dom';
import { getBookings } from '../services/apiBookings';
import Toolbar from './Toolbar';

const StyledFilter = styled.div`
  border: 1px solid var(--color-grey-100);
  background-color: var(--color-grey-0);
  box-shadow: var(--shadow-sm);
  border-radius: var(--border-radius-sm);
  padding: 0.4rem;
  display: flex;
  align-items: center;

  gap: 0.4rem;
`;

const FilterButton = styled.button`
  background-color: var(--color-grey-0);
  border: none;

  ${props =>
    props.$active &&
    css`
      border-color: var(--color-brand-600);
      color: var(--color-brand-600);
      border-style: solid;
    `}

  border-radius: var(--border-radius-md);
  font-weight: 500;
  font-size: 1.4rem;
  /* To give the same height as select */
  padding: 0.44rem 0.8rem;
  transition: all 0.3;

  &:hover:not(:disabled) {
    background-color: var(--color-brand-600);
    color: var(--color-brand-50);
  }
`;

const FilterLabel = styled.div`
  font-weight: 500;
  font-size: 1.6rem;
  /* To give the same height as select */
  padding: 0.44rem 0.8rem;
`;

function Filter({ filterField, options }) {
  const [searchParams, setSearchParams] = useSearchParams();
  const currentFilter = searchParams.get(filterField) || options.at(0).value;

  function handleClick(value) {
    if (value === 'all') {
      searchParams.delete(filterField);
    } else {
      searchParams.set(filterField, value);
    }
    setSearchParams(searchParams);
  }

  return (
    <StyledFilter>
      <FilterLabel>
        {filterField.charAt(0).toUpperCase() + filterField.slice(1) + ':'}
      </FilterLabel>
      {options.map(option => (
        <Toolbar.Button
          $variation="secondary"
          $size="medium"
          key={option.value}
          onClick={() => handleClick(option.value)}
          $active={option.value === currentFilter}
          disabled={option.value === currentFilter}>
          {option.label}
        </Toolbar.Button>
      ))}
      {/* {options.map(option => (
        <FilterButton
          key={option.value}
          onClick={() => handleClick(option.value)}
          $active={option.value === currentFilter}
          disabled={option.value === currentFilter}>
          {option.label}
        </FilterButton>
      ))} */}
    </StyledFilter>
  );
}

export default Filter;

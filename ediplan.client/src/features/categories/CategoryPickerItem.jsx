import styled, { css } from 'styled-components';

const StyledButton = styled.button`
  box-sizing: border-box;
  height: 3.3rem;
  display: flex;
  justify-content: flex-start;
  align-items: center;

  padding: 0.5rem 2rem;
  border: 1px solid var(--color-grey-300);
  background-color: var(--color-brand-50);
  border-radius: 3px;
  transition:
    background-color 0.1s,
    border 0.15s;

  &:hover {
    background-color: var(--color-brand-100);
  }

  &:focus {
    outline: none;
  }

  ${props =>
    props.isSelected === true &&
    css`
      background-color: var(--color-brand-300);
      border: 2px solid var(--color-brand-400);
      &:hover {
        background-color: var(--color-brand-400);
      }
    `}
`;

function CategoryPickerItem({ category, handleClick, isSelected }) {
  return (
    <StyledButton type="button" onClick={handleClick} isSelected={isSelected}>
      {category}
    </StyledButton>
  );
}

export default CategoryPickerItem;

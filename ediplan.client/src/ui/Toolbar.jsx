import styled, { css } from 'styled-components';

const StyledToolbar = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 0.2rem;
  height: fit-content;
  border: 1px solid var(--color-grey-200);
  background-color: var(--color-grey-50);
  border-radius: var(--border-radius-md);
  padding: 0.4rem 0.2rem;
  box-shadow: var(--shadow-md);
  margin-bottom: 0.4rem;
`;

const StyledToolbarPanel = styled.div`
  display: flex;

  ${props =>
    props.side === 'left' &&
    css`
      grid-column: 1;
      grid-row: 1;
      padding-left: 0.4rem;
    `}

  ${props =>
    props.side === 'right' &&
    css`
      grid-column: 2;
      grid-row: 1;
      justify-content: flex-end;
      padding-right: 0.4rem;
    `}
`;

const buttonSizes = {
  small: css`
    font-size: 1.2rem;
    padding: 0.4rem 1.2rem;
    text-transform: uppercase;
    font-weight: 600;
    text-align: center;
  `,
  medium: css`
    font-size: 1.4rem;
    padding: 0.4rem 1.2rem;
    font-weight: 500;
  `,
  large: css`
    font-size: 1.6rem;
    padding: 0.4rem 2.4rem;
    font-weight: 500;
  `,
};

const buttonVariations = {
  primary: css`
    color: var(--color-grey-600);
    background: var(--color-grey-0);
    border: 1px solid var(--color-grey-200);
    transition: all 0.3s;
    /* box-shadow: var(--box-shadow-md); */

    &:hover:not(:disabled) {
      background-color: var(--color-brand-600);
      color: var(--color-brand-50);
      & svg {
        width: 2.4rem;
        height: 2.4rem;
        color: var(--color-grey-100);
      }
    }

    & svg {
      width: 2.4rem;
      height: 2.4rem;
      color: var(--color-grey-700);
    }

    ${props =>
      props.$active &&
      css`
        background-color: var(--color-brand-600);
        border-color: var(--color-brand-600);
        color: var(--color-brand-50);
        border: 2px solid var(--color-brand-600);
        & svg {
          width: 2.4rem;
          height: 2.4rem;
          color: var(--color-brand-50);
        }
      `}
  `,
  secondary: css`
    color: var(--color-grey-600);
    background: var(--color-grey-0);
    border: 1px solid var(--color-grey-100);
    transition: all 0.3s;

    &:hover:not(:disabled) {
      background-color: var(--color-brand-600);
      color: var(--color-brand-50);
      & svg {
        width: 2.4rem;
        height: 2.4rem;
        color: var(--color-grey-100);
      }
    }

    & svg {
      width: 2.4rem;
      height: 2.4rem;
      color: var(--color-grey-700);
    }

    ${props =>
      props.$active &&
      css`
        border-color: var(--color-brand-600);
        color: var(--color-brand-600);
        border: 3px solid var(--color-brand-600);
      `}
  `,
  danger: css`
    color: var(--color-red-100);
    background-color: var(--color-red-700);

    &:hover {
      background-color: var(--color-red-800);
    }
  `,
};

const Button = styled.button`
  border: none;
  border-radius: var(--border-radius-sm);
  box-shadow: var(--shadow-sm);
  margin: 0 0.3rem;

  ${props => buttonSizes[props.$size]}
  ${props => buttonVariations[props.$variation]}
`;

Button.defaultProps = {
  $variation: 'primary',
  $size: 'medium',
};

function Toolbar({ children }) {
  return <StyledToolbar>{children}</StyledToolbar>;
}

function Panel({ side, children }) {
  return <StyledToolbarPanel side={side}>{children}</StyledToolbarPanel>;
}

Toolbar.Panel = Panel;
Toolbar.Button = Button;

export default Toolbar;

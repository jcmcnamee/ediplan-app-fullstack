/* eslint-disable no-unused-vars */
import styled, { css } from 'styled-components';

const sizes = {
  small: css`
    font-size: 1.2rem;
    padding: 0.4rem 0.8rem;
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
    padding: 1.2rem 2.4rem;
    font-weight: 500;
  `,
};

const variations = {
  primary: css`
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

const ToolButton = styled.button`
  border: none;
  border-radius: var(--border-radius-sm);
  box-shadow: var(--shadow-sm);

  ${props => sizes[props.$size]}
  ${props => variations[props.$variation]}
`;

ToolButton.defaultProps = {
  $variation: 'primary',
  $size: 'medium',
};

export default ToolButton;

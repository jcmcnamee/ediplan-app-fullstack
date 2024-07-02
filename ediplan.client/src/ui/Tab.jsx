import { NavLink } from 'react-router-dom';
import styled from 'styled-components';

const StyledNavLink = styled(NavLink)`
  &:link,
  &:visited {
    display: flex;
    align-items: center;

    color: var(--color-grey-600);
    font-size: 1.6rem;
    font-weight: 500;
    padding: 0.1rem 1.5rem;
    transition: all 0.3s;

    border-width: 1px 1px 0 1px;
    border-style: solid;
    border-color: var(--color-brand-200);
    border-radius: var(--border-radius-md) var(--border-radius-md) 0 0;
    box-shadow: var(--shadow-tab-inactive);
  }

  &:hover,
  &:active,
  &:focus,
  &.active:link,
  &.active:visited,
  &.active:focus {
    color: var(--color-grey-800);
    background-color: var(--color-brand-100);
    z-index: 1;
    padding: 0.4rem 1.5rem;
    position: relative;
    bottom: -1px;
    box-shadow: var(--shadow-tab-active);
  }
`;

function Tab({ route, children }) {
  return (
    <li>
      <StyledNavLink to={route} tabIndex={0}>
        {children}
      </StyledNavLink>
    </li>
  );
}

export default Tab;

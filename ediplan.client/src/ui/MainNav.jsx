import { NavLink } from 'react-router-dom';
import styled from 'styled-components';
import {
  LuBookOpenCheck,
  LuBox,
  LuBoxes,
  LuClapperboard,
  LuGanttChartSquare,
  LuLayoutDashboard,
  LuSettings,
} from 'react-icons/lu';

const NavList = styled.ul`
  display: flex;
  flex-direction: column;
  gap: 0.2rem;
`;

const StyledNavLink = styled(NavLink)`
  &:link,
  &:visited {
    display: flex;
    align-items: center;
    gap: 1.2rem;

    color: var(--color-grey-600);
    font-size: 1.6rem;
    font-weight: 500;
    padding: 1.2rem 2.4rem;
    transition: all 0.3s;
  }

  &:hover,
  &:active,
  &.active:link,
  &.active:visited {
    color: var(--color-grey-800);
    background-color: var(--color-grey-200);
    border-radius: var(--border-radius-sm);
  }

  & svg {
    width: 2.4rem;
    height: 2.4rem;
    color: var(--color-grey-400);
    transition: all 0.3s;
  }

  &:hover svg,
  &:active svg,
  &.active:link svg,
  &.active:visited svg {
    color: var(--color-brand-700);
  }
`;

function MainNav() {
  return (
    <nav>
      <NavList>
        <li>
          <StyledNavLink to="./dashboard">
            <LuLayoutDashboard />
            Dashboard
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="./timeline">
            <LuGanttChartSquare />
            Timeline
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="./bookings">
            <LuBookOpenCheck />
            Bookings
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="./assets">
            <LuBox />
            Assets
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="./groups">
            <LuBoxes />
            Groups
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="./productions">
            <LuClapperboard />
            Productions
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="./settings">
            <LuSettings />
            Settings
          </StyledNavLink>
        </li>
      </NavList>
    </nav>
  );
}

export default MainNav;

import { createContext, useContext } from 'react';
import { createPortal } from 'react-dom';
import { LuSlidersHorizontal } from 'react-icons/lu';
import styled from 'styled-components';
import { useOutsideClick } from '../../hooks/useOutsideClick';

import Toolbar from '../Toolbar';

const StyledList = styled.ul`
  position: fixed;

  background-color: var(--color-grey-0);
  box-shadow: var(--shadow-md);
  border-radius: var(--border-radius-md);

  right: ${props => props.$position.x}px;
  top: ${props => props.$position.y}px;
`;

const FilterMenuContext = createContext();

function FilterMenu({ isOpen, setIsOpen, position, setPosition, children }) {
  return (
    <FilterMenuContext.Provider
      value={{ isOpen, setIsOpen, position, setPosition }}
    >
      {children}
    </FilterMenuContext.Provider>
  );
}

// Menu card
const Menu = styled.div`
  display: flex;
  align-items: center;
  justify-content: flex-end;
`;

function Toggle() {
  const { isOpen, setIsOpen, setPosition } = useContext(FilterMenuContext);

  function handleClick(e) {
    const rect = e.target.closest('button').getBoundingClientRect();

    setPosition({
      x: window.innerWidth - rect.width - rect.x,
      y: rect.y + rect.height + 8
    });

    setIsOpen(s => !s);
  }

  return (
    <Toolbar.Button
      $variation="primary"
      $size="medium"
      $active={isOpen}
      onClick={handleClick}
    >
      <LuSlidersHorizontal />
    </Toolbar.Button>
  );
}

function List({ children }) {
  const { isOpen, position, setIsOpen } = useContext(FilterMenuContext);
  const ref = useOutsideClick(() => setIsOpen(false));

  if (!isOpen) return null;

  return createPortal(
    <StyledList $position={position} ref={ref}>
      {children}
    </StyledList>,
    document.body
  );
}

FilterMenu.Menu = Menu;
FilterMenu.Toggle = Toggle;
FilterMenu.List = List;
// FilterMenu.Button = Button;

export default FilterMenu;

import styled from 'styled-components';

const StyledTabList = styled.ul`
  display: flex;
  gap: 1px;
  align-items: flex-end;
`;

function TabContainer({ children }) {
  return (
    <div>
      <StyledTabList>{children}</StyledTabList>
    </div>
  );
}

export default TabContainer;

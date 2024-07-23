import styled from 'styled-components';

const StyledWarning = styled.span`
  color: red;
  font-weight: strong;
`;

function Warning({ children }) {
  return <StyledWarning>{children}</StyledWarning>;
}

export default Warning;

import styled from "styled-components";

const StyledCell = styled.div`
  display: flex;
  flex-direction: column;
`;

function StackedCell({ data1, data2 }) {
  return (
    <StyledCell>
      <div>{data1}</div>
      <div>{data2}</div>
    </StyledCell>
  );
}

export default StackedCell;

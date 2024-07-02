import styled from "styled-components";
import { LuArrowDownUp } from "react-icons/lu";
import { useSearchParams } from "react-router-dom";

import Table from "../../ui/Table";

const Button = styled.button``;

function AssetHeaderItem({ item }) {
  const [searchParams, setSearchParams] = useSearchParams();

  function handleClick() {
    searchParams.set("sortBy");
  }

  return (
    <Table.HeaderItem>
      <div>{item}</div>
      <Button>
        <LuArrowDownUp onClick={handleClick} />
      </Button>
    </Table.HeaderItem>

  );
}

export default AssetHeaderItem;

import { useSearchParams } from "react-router-dom";
import { LuArrowDownUp } from "react-icons/lu";
import Table from "../../ui/Table";
import { useState } from "react";

function BookingHeaderItem({ data }) {
  const [searchParams, setSearchParams] = useSearchParams();
  const [sortDirection, setSortDirection] = useState("desc");

  function handleClick(data, sortDirection) {
    searchParams.set("sortBy", `${data.toLowerCase()} ${sortDirection}`);
    setSearchParams(searchParams);
  }

  return (
    <Table.HeaderItem>
      <div>{data}</div>
      <button>
        <LuArrowDownUp onClick={() => handleClick(data, sortDirection)} />
      </button>
    </Table.HeaderItem>
  );
}

export default BookingHeaderItem;

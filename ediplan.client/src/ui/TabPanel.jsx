import { useEffect, useState } from "react";
import styled from "styled-components";
import Tab from "./Tab";
import { useSearchParams } from "react-router-dom";

const StyledTabList = styled.ul`
  display: flex;
  gap: 1px;
  align-items: flex-end;
`;

function TabPanel({ tabs }) {
  const [searchParams, setSearchParams] = useSearchParams();
  const [activeTab, setActiveTab] = useState("equip");

  useEffect(() => {
    // Update active tab based on searchParams or default to "equip"
    const category = searchParams.get("category");
    setActiveTab(category || "equip");
  }, [searchParams]);

  function handleClick(key) {
    setActiveTab(key);
    searchParams.set("category", key);
    setSearchParams(searchParams);
  }

  return (
    <StyledTabList>
      {tabs.map((tab) => (
        <Tab
          title={tab.title}
          index={tab.query}
          className={tab.query === activeTab ? "active" : ""}
          onClick={handleClick}
          key={tab.query}
        />
      ))}
    </StyledTabList>
  );
}

export default TabPanel;

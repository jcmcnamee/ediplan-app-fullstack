import { useMemo } from 'react';
import Toolbar from '../../ui/Toolbar';
import Input from '../../ui/Form/Input';
import FilterMenu from '../../ui/Table/FilterMenu';
import Filter from '../../ui/Filter';

function TableToolbar({
  isFilterOpen,
  setIsFilterOpen,
  filterPosition,
  setFilterPosition,
}) {
  const memoizedToolbar = useMemo(
    () => (
      <Toolbar>
        <Toolbar.Panel side="left">
          <Input />
        </Toolbar.Panel>
        <Toolbar.Panel side="right">
          <FilterMenu
            isOpen={isFilterOpen}
            setIsOpen={setIsFilterOpen}
            position={filterPosition}
            setPosition={setFilterPosition}
          >
            <FilterMenu.Toggle />
            <FilterMenu.List>
              <Filter
                filterField="Type"
                options={[
                  { value: 'all', label: 'All' },
                  { value: 'equipment', label: 'Equipment' },
                  { value: 'person', label: 'Person' },
                  { value: 'room', label: 'Room' },
                ]}
              />
            </FilterMenu.List>
          </FilterMenu>
        </Toolbar.Panel>
      </Toolbar>
    ),
    [filterPosition, isFilterOpen],
  );

  return memoizedToolbar;
}

export default TableToolbar;

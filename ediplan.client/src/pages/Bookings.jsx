import { LuBookPlus, LuCalendar } from 'react-icons/lu';
import BookingTable from '../features/bookings/BookingTable';
import Filter from '../ui/Filter';
import FilterMenu from '../ui/Table/FilterMenu';
import Toolbar from '../ui/Toolbar';
import { NavLink, Outlet, useLocation } from 'react-router-dom';
import Input from '../ui/Form/Input';
import { useState } from 'react';

function Bookings() {
  const pathSegments = useLocation()
    .pathname.split('/')
    .filter(str => str !== '' && str !== undefined);
  const isChildRoute = pathSegments.pop() !== 'bookings';

  const [filterPosition, setFilterPosition] = useState({ x: 0, y: 0 });
  const [isFilterOpen, setIsFilterOpen] = useState(false);

  return (
    <>
      <h1>Bookings</h1>
      {isChildRoute ? (
        <>
          <Outlet />
        </>
      ) : (
        <>
          <Toolbar>
            <Toolbar.Panel side="right">
              <FilterMenu
                position={filterPosition}
                setPosition={setFilterPosition}
                isOpen={isFilterOpen}
                setIsOpen={setIsFilterOpen}
              >
                <FilterMenu.Menu>
                  <FilterMenu.Toggle />
                  <FilterMenu.List>
                    <Filter
                      filterField="status"
                      options={[
                        { value: 'all', label: 'All' },
                        { value: 'confirmed', label: 'Confirmed' },
                        { value: 'provisional', label: 'Provisional' }
                      ]}
                    />
                  </FilterMenu.List>
                </FilterMenu.Menu>
              </FilterMenu>
            </Toolbar.Panel>
            <Toolbar.Panel side="left">
              <Input />
              <Toolbar.Button $variation="primary">
                <NavLink to="create">
                  <LuBookPlus />
                </NavLink>
              </Toolbar.Button>
            </Toolbar.Panel>
          </Toolbar>
          <BookingTable />
        </>
      )}
    </>
  );
}

export default Bookings;

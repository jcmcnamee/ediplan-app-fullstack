import { useEffect, useMemo, useState } from 'react';
import { useAssets } from '../assets/useAssets';
import Spinner from '../../ui/Spinner';
import Empty from '../../ui/Empty';
import MiniTableLayout from '../../ui/Table/MiniTableLayout';
import MiniTable from '../assets/MiniTable';
import Toolbar from '../../ui/Toolbar';
import FilterMenu from '../../ui/Table/FilterMenu';
import Filter from '../../ui/Filter';
import Input from '../../ui/Form/Input';
import {
  LuArrowBigLeft,
  LuArrowBigLeftDash,
  LuArrowBigRight,
  LuArrowBigRightDash,
} from 'react-icons/lu';
import Button from '../../ui/Button';

function BookingAssetPicker({selectedAssets, setSelectedAssets}) {
  const [isFilterOpen, setIsFilterOpen] = useState(false);
  const [filterPosition, setFilterPosition] = useState(null);
  // const [selectedAssets, setSelectedAssets] = useState({});

  useEffect(() => {
    console.log('isFilterOpen changed:', isFilterOpen);
  }, [isFilterOpen]);

  const {
    assets,
    links,
    paginationHeaderData,
    error,
    isPending,
    fetchPage,
    getLink,
  } = useAssets();

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
            setPosition={setFilterPosition}>
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
    [filterPosition, isFilterOpen]
  );

  if (error) return <div>{error}</div>;
  if (isPending) return <Spinner />;

  const data = assets;

  if (!data.length) return <Empty resource="bookings" />;

  // const prevLink = getLink('prev');
  // const nextLink = getLink('next');

  return (
    <div>
      {/* <Toolbar>
        <Toolbar.Panel side="left">
          <Input />
        </Toolbar.Panel>
        <Toolbar.Panel side="right">
          <FilterMenu>
            <FilterMenu.Toggle />
            <FilterMenu.List>
              <Filter
                filterField="addType"
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
      </Toolbar> */}
      {memoizedToolbar}
      <MiniTable
        tableData={data}
        rowSelection={selectedAssets}
        setRowSelection={setSelectedAssets}
        pageCount={paginationHeaderData.totalPages}
      />
      <div>
        <Button variation="secondary" size="small">
          <LuArrowBigLeftDash />
        </Button>
        <Button
          variation="secondary"
          size="small"
          onClick={() => fetchPage('previous')}>
          <LuArrowBigLeft />
        </Button>
        <span>
          {' '}
          {paginationHeaderData.currentPage} / {paginationHeaderData.totalPages}{' '}
        </span>
        <Button
          variation="secondary"
          size="small"
          onClick={() => fetchPage('next')}>
          <LuArrowBigRight />
        </Button>
        <Button variation="secondary" size="small">
          <LuArrowBigRightDash />
        </Button>
      </div>
    </div>
  );
}

export default BookingAssetPicker;

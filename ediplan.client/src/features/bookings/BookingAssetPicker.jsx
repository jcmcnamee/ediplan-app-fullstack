import { useEffect, useMemo, useState } from 'react';
import { useAssets } from '../assets/useAssets';
import Spinner from '../../ui/Spinner';
import Empty from '../../ui/Empty';
import MiniTableLayout from '../../ui/Table/MiniTableLayout';
import MiniTable from '../assets/AssetSelectorTable';
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
import useAssetFilters from '../assets/useAssetFilters';
import { assetKeys } from '../assets/assetQueries';
import AssetPickerTable from '../assets/AssetSelectorTable';
import AssetSelectedTable from '../assets/AssetSelectedTable';
import AssetSelectorTable from '../assets/AssetSelectorTable';

function BookingAssetPicker({
  confirmedAssets,
  setConfirmedAssets,
  state,
  dispatch,
}) {
  const [isFilterOpen, setIsFilterOpen] = useState(false);
  const [filterPosition, setFilterPosition] = useState(null);
  const [selectedAssets, setSelectedAssets] = useState([]);
  // const [assetsToRemove, setAssetsToRemove] = useState([]);
  // const { state, dispatch } = useAssetFilters();

  useEffect(() => {
    console.log('isFilterOpen changed:', isFilterOpen);
  }, [isFilterOpen]);

  const tableParams = Object.fromEntries(
    Object.entries(state).filter(([_, value]) => value != null),
  );

  const { assets, paginationHeaderData, error, isPending } = useAssets(
    assetKeys.filter(tableParams),
  );

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

  if (error) return <div>{error}</div>;
  if (isPending) return <Spinner />;

  const data = assets;

  if (!data.length) return <Empty resource="bookings" />;

  // const prevLink = getLink('prev');
  // const nextLink = getLink('next');

  const assetsToAdd = data.filter((obj) => selectedAssets[obj.id] === true);
  console.log('Select assets: ', selectedAssets);
  console.log('Assets to add: ', assetsToAdd);

  return (
    <div>
      {memoizedToolbar}
      <span>
        <AssetSelectorTable
          tableData={data}
          rowSelection={selectedAssets}
          setRowSelection={setSelectedAssets}
          pageCount={paginationHeaderData.totalPages}
        />
        <AssetSelectedTable tableData={assetsToAdd} />
      </span>
      <div>
        <Button variation="secondary" size="small">
          <LuArrowBigLeftDash />
        </Button>
        <Button
          variation="secondary"
          size="small"
          onClick={() => dispatch({ type: 'prevPage' })}
        >
          <LuArrowBigLeft />
        </Button>
        <span>
          {' '}
          {paginationHeaderData.currentPage} / {paginationHeaderData.totalPages}{' '}
        </span>
        <Button
          variation="secondary"
          size="small"
          onClick={() => dispatch({ type: 'nextPage' })}
        >
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

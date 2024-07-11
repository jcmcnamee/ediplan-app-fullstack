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
  LuArrowBigRightDash
} from 'react-icons/lu';
import Button from '../../ui/Button';
import useAssetFilters from '../assets/useAssetFilters';
import { assetKeys } from '../assets/assetQueries';
import AssetPickerTable from '../assets/AssetSelectorTable';
import AssetSelectedTable from '../assets/AssetSelectedTable';
import AssetSelectorTable from '../assets/AssetSelectorTable';
import TableToolbar from './TableToolbar';

function BookingAssetPicker({
  selectedAssets,
  setSelectedAssets,
  state,
  dispatch
}) {
  const [isFilterOpen, setIsFilterOpen] = useState(false);
  const [filterPosition, setFilterPosition] = useState(null);
  const [selectedRowIds, setSelectedRowIds] = useState({});
  const [rowIdtoRemove, setRowIdToRemove] = useState([]);
  // const [selectedAssets, setSelectedAssets] = useState([]);

  useEffect(() => {
    console.log('isFilterOpen changed:', isFilterOpen);
  }, [isFilterOpen]);

  const tableParams = Object.fromEntries(
    Object.entries(state).filter(([_, value]) => value != null)
  );

  const { assets, paginationHeaderData, error, isPending } = useAssets(
    assetKeys.filter(tableParams)
  );

  const handleToggleRowSelection = (rowId, rowData) => {
    console.log('Row data: ', rowData);
    // Update table row selection state
    setSelectedRowIds(prev => {
      // Create a shallow copy of the previous state
      const newSelection = { ...prev };

      if (newSelection[rowId]) {
        // If the row is currently selected, delete the key to deselect
        delete newSelection[rowId];
      } else {
        // Otherwise, add the key to select the row
        newSelection[rowId] = true;
      }
      return newSelection;
    });

    // Persist selected asset data in state due to server pagination
    setSelectedAssets(prev => {
      // Check if the row is already selected
      const isSelected = prev.some(selectedRow => selectedRow.id === rowId);

      if (isSelected) {
        // If it is selected, remove it from the array
        return prev.filter(selectedRow => selectedRow.id !== rowId);
      } else {
        console.log('Adding asset: ', rowData);
        // If it is not selected, add it to the array
        return [...prev, rowData];
      }
    });
    console.log('Selected assets: ', selectedAssets);
  };

  if (error) return <div>{error}</div>;
  if (isPending) return <Spinner />;

  if (!assets.length) return <Empty resource="bookings" />;

  // const prevLink = getLink('prev');
  // const nextLink = getLink('next');

  const assetsForSelection = assets.filter(asset => !selectedRowIds[asset.id]);
  // const selectedAssets = assets.filter(obj => selectedRowIds[obj.id] === true);

  return (
    <div>
      <TableToolbar
        isFilterOpen={isFilterOpen}
        setIsFilterOpen={setIsFilterOpen}
        filterPosition={filterPosition}
        setFilterPosition={setFilterPosition}
      />
      <span>
        <AssetSelectorTable
          tableData={assetsForSelection}
          rowSelection={selectedRowIds}
          setRowSelection={setSelectedRowIds}
          toggleRowSelection={handleToggleRowSelection}
          pageCount={paginationHeaderData.totalPages}
        />
        <AssetSelectedTable
          tableData={selectedAssets}
          rowSelection={rowIdtoRemove}
          setRowSelection={setRowIdToRemove}
          toggleRowSelection={handleToggleRowSelection}
        />
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

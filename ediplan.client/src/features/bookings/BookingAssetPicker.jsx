import { useEffect, useMemo, useState } from 'react';
import { useAssets } from '../assets/useAssets';
import Spinner from '../../ui/Spinner';
import Empty from '../../ui/Empty';
import MiniTableLayout from '../../ui/Table/MiniTableLayout';
import MiniTable from './AssetSelectorTable';
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
import AssetPickerTable from './AssetSelectorTable';
import AssetSelectedTable from './AssetSelectedTable';
import AssetSelectorTable from './AssetSelectorTable';
import TableToolbar from './AssetPickerToolbar';
import styled from 'styled-components';
import AssetPickerToolbar from './AssetPickerToolbar';

const StyledTableContainer = styled.div`
  display: flex;
`;

function BookingAssetPicker({
  selectedAssets,
  setSelectedAssets,
  formState,
  dispatch,
  toggleTables,
  showTables,
  showLockWarning
}) {
  const [isFilterOpen, setIsFilterOpen] = useState(false);
  const [filterPosition, setFilterPosition] = useState(null);
  const [selectedRowIds, setSelectedRowIds] = useState({});
  const [rowIdtoRemove, setRowIdToRemove] = useState([]);

  // Get only not null filter params
  const tableParams = Object.fromEntries(
    Object.entries(formState).filter(([_, value]) => value != null)
  );

  // Get filtered Assets
  const { assets, paginationHeaderData, error, isPending } = useAssets(
    assetKeys.filter(tableParams)
  );

  // Declare handlers
  const handleToggleRowSelection = (rowId, rowData) => {
    // Update table row selection state to keep integrity
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

    // Persist selected asset data in state due to server pagination.
    // Not sure if this was the best way but it works for now.
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
  };

  // Render logic
  if (error) return <div>{error}</div>;
  if (isPending) return <Spinner />;

  if (!assets.length) return <Empty resource="bookings" />;

  // HATOAS links
  // const prevLink = getLink('prev');
  // const nextLink = getLink('next');

  // Filter Assets to exclude already selected assets
  const assetsForSelection = assets.filter(asset => !selectedRowIds[asset.id]);

  return (
    <div style={{ overflow: 'hidden' }}>
      <AssetPickerToolbar
        isFilterOpen={isFilterOpen}
        setIsFilterOpen={setIsFilterOpen}
        filterPosition={filterPosition}
        setFilterPosition={setFilterPosition}
        toggleTables={toggleTables}
        showFilters={showTables}
        showLockWarning={showLockWarning}
      />
      {showTables && (
        <StyledTableContainer>
          <AssetSelectedTable
            tableData={selectedAssets}
            rowSelection={rowIdtoRemove}
            setRowSelection={setRowIdToRemove}
            toggleRowSelection={handleToggleRowSelection}
          />
          <AssetSelectorTable
            tableData={assetsForSelection}
            rowSelection={selectedRowIds}
            setRowSelection={setSelectedRowIds}
            toggleRowSelection={handleToggleRowSelection}
            pageCount={paginationHeaderData.totalPages}
          />
        </StyledTableContainer>
      )}
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

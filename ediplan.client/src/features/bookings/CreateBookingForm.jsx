import Button from '../../ui/Button';
import Form from '../../ui/Form/Form';

import { useState } from 'react';
import { FormProvider, useForm } from 'react-hook-form';
import { useCategories } from '../categories/useCategories';

import BookingAssetPicker from './BookingAssetPicker';
import useAssetFilters from '../assets/useAssetFilters';

import BookingCategoryPicker from '../categories/MultiItemPicker';
import Spinner from '../../ui/Spinner';
import { useCreateBooking } from './useCreateBooking';
import MultiItemPicker from '../categories/MultiItemPicker';

function CreateBookingForm() {
  const [showAssets, setShowAssets] = useState(false);
  const [showLockWarning, setShowLockWarning] = useState(false);
  const [selectedAssets, setSelectedAssets] = useState([]);
  const [selectedCategoryIds, setSelectedCategoryIds] = useState([]);
  const { categories, error, isPending } = useCategories();
  const { isCreating, apiCreateBooking } = useCreateBooking();
  // Assets are filtered in the Asset Picker depending on date input hence passing these down as props
  const { state, dispatch } = useAssetFilters();

  const methods = useForm();

  const handleToggleTables = () => {
    if ((state.from !== null) & (state.to !== null)) {
      setShowAssets(s => !s);
      setShowLockWarning(false);
    } else {
      setShowLockWarning(true);
    }
  };

  const onSubmit = data => {
    data.assetIds = selectedAssets.map(a => a.id);
    data.bookingGroupIds = selectedCategoryIds;
    apiCreateBooking(data, { onSuccess: () => methods.reset() });
  };

  const onError = errors => {
    console.error('Errors: ', errors);
  };

  console.log('Category IDs: ', selectedCategoryIds);

  if (error) return <div>{error}</div>;
  if (isPending) return <Spinner />;

  return (
    <>
      <FormProvider {...methods}>
        <Form
          onSubmit={methods.handleSubmit(onSubmit, onError)}
          id="createBookingForm"
        >
          <Form.TextShort
            label="Booking name: "
            id="name"
            placeholder="Booking name...."
            validation={{ required: 'Name is required' }}
            disabled={isCreating}
          />
          <Form.Checkbox label="Provisional: " id="provisional" side="right" />
          <Form.DateSelect
            label="Start: "
            id="startDate"
            dispatch={dispatch}
            action="filterFromDate"
            disabled={isCreating}
          />
          <Form.DateSelect
            label="End: "
            id="endDate"
            dispatch={dispatch}
            action="filterToDate"
            disabled={isCreating}
          />
          <Form.TextLong label="Notes: " id="notes" disabled={isCreating} />
          <MultiItemPicker
            items={categories}
            allowCreateItems={true}
            selectedItemIds={selectedCategoryIds}
            setSelectedItemIds={setSelectedCategoryIds}
          />
        </Form>
      </FormProvider>
      <BookingAssetPicker
        selectedAssets={selectedAssets}
        setSelectedAssets={setSelectedAssets}
        formState={state}
        dispatch={dispatch}
        toggleTables={handleToggleTables}
        showTables={showAssets}
        showLockWarning={showLockWarning}
      />
      <Button
        variation="primary"
        size="medium"
        type="submit"
        form="createBookingForm"
        disabled={isCreating}
      >
        Create booking
      </Button>
    </>
  );
}

export default CreateBookingForm;

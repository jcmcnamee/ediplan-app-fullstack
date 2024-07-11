import { LuPackagePlus } from 'react-icons/lu';
import Button from '../../ui/Button';
import Form from '../../ui/Form/Form';
import Toolbar from '../../ui/Toolbar';
import { useState } from 'react';
import { FormProvider, useForm } from 'react-hook-form';

import BookingAssetPicker from './BookingAssetPicker';
import useAssetFilters from '../assets/useAssetFilters';

function CreateBookingForm() {
  const [showAssets, setShowAssets] = useState(false);
  const [selectedAssets, setSelectedAssets] = useState([]);
  const { state, dispatch } = useAssetFilters();

  console.log('Selected assets: ', selectedAssets);

  const methods = useForm();

  const onSubmit = data => console.log(data);

  const handleToggleAssets = () => {
    setShowAssets(s => !s);
    console.log('Toggling: ');
  };

  return (
    <>
      <FormProvider {...methods}>
        <Form onSubmit={methods.handleSubmit(onSubmit)}>
          <Form.TextShort
            label="Booking name: "
            id="name"
            placeholder="Booking name...."
          />
          <Form.Checkbox label="Provisional: " id="provisional" side="right" />
          <Form.DateSelect
            label="Start: "
            id="startDate"
            dispatch={dispatch}
            action="filterFromDate"
          />
          <Form.DateSelect
            label="End: "
            id="endDate"
            dispatch={dispatch}
            action="filterToDate"
          />
          <Form.TextLong label="Notes: " id="description" />
          <Form.HiddenInput
            id="assetIds"
            value={selectedAssets.map(asset => `${asset.id},`)}
          />
        </Form>
      </FormProvider>
      <Toolbar>
        <Toolbar.Panel side="left">
          <Toolbar.Button $variation="primary" onClick={handleToggleAssets}>
            <LuPackagePlus />
          </Toolbar.Button>
          <span>Please select dates...</span>
        </Toolbar.Panel>
        <Toolbar.Panel side="right">
          {!showAssets ?? (
            <Button
              variation="primary"
              size="medium"
              onClick={methods.handleSubmit(onSubmit)}
            >
              Create booking
            </Button>
          )}
        </Toolbar.Panel>
      </Toolbar>
      <div>
        {showAssets ? (
          <div>
            <BookingAssetPicker
              selectedAssets={selectedAssets}
              setSelectedAssets={setSelectedAssets}
              state={state}
              dispatch={dispatch}
            />
            <Button
              variation="primary"
              size="medium"
              onClick={methods.handleSubmit(onSubmit)}
            >
              Create booking
            </Button>
          </div>
        ) : (
          'No assets....'
        )}
      </div>
    </>
  );
}

export default CreateBookingForm;

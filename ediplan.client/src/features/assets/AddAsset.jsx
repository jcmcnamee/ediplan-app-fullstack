import Button from "../../ui/Button";
import CreateAssetForm from "./CreateAssetForm";
import Modal from "../../ui/Modal";

function AddAsset({ category }) {
  return (
    <Modal>
      <Modal.Open opensWindowName="asset-form">
        <Button>Add new asset</Button>
      </Modal.Open>
      <Modal.Window name="asset-form">
        <CreateAssetForm category={category} />
      </Modal.Window>
    </Modal>
  );
}

export default AddAsset;
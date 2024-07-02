import { LuCopy, LuPencil, LuTrash2 } from "react-icons/lu";
import Menus from "../../ui/Menus";
import Modal from "../../ui/Modal";

function BookingTableMenu({ bookingId }) {
  function handleDuplicate() {
    // createAsset({ ...asset });
    console.log("Make duplicate!");
  }

  return (
    <Modal>
      <Menus.Menu>
        <Menus.Toggle id={bookingId} />
        <Menus.List id={bookingId}>
          <Menus.Button icon={<LuCopy />} onClick={handleDuplicate}>
            Duplicate
          </Menus.Button>
          <Modal.Open opensWindowName="edit">
            <Menus.Button icon={<LuPencil />}>Edit</Menus.Button>
          </Modal.Open>
          <Modal.Open opensWindowName="delete">
            <Menus.Button icon={<LuTrash2 />}>Delete</Menus.Button>
          </Modal.Open>
        </Menus.List>
      </Menus.Menu>

      <Modal.Window name="edit">
        <div>Edit booking {bookingId}</div>
      </Modal.Window>

      <Modal.Window name="delete">
        <div>Delete booking {bookingId}</div>
      </Modal.Window>
    </Modal>
  );
}

export default BookingTableMenu;

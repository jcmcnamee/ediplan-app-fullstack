import styled from 'styled-components';
import CategoryPickerItem from './CategoryPickerItem';

const StyledItemContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 2px;

  grid-row: 2;
  grid-column: 1 / 3;
`;

const StyledPickerContainer = styled.div`
  display: grid;
  grid-template-rows: 3rem auto;
  grid-template-columns: 3fr 1fr;
  max-width: 350px;

  grid-column: 2;
  grid-row: 2 / 4;

  overflow: auto;
`;

const StyledPickerTitle = styled.div`
  grid-column: 1 / 2;
  grid-row: 1 / 2;
`;

function MultiItemPicker({
  items,
  allowCreateItems,
  selectedItemIds,
  setSelectedItemIds
}) {
  const toggleItem = id => {
    if (selectedItemIds.includes(id)) {
      setSelectedItemIds(selectedItemIds.filter(c => c !== id));
    } else {
      setSelectedItemIds([...selectedItemIds, id]);
    }
  };

  return (
    <StyledPickerContainer>
      <StyledPickerTitle>Categories:</StyledPickerTitle>

      <StyledItemContainer>
        {items.data.map(i => (
          <CategoryPickerItem
            key={i.id}
            category={i.name}
            handleClick={() => toggleItem(i.id)}
            isSelected={selectedItemIds.includes(i.id)}
          />
        ))}
      </StyledItemContainer>
    </StyledPickerContainer>
  );
}

export default MultiItemPicker;

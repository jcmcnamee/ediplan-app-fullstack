import styled from 'styled-components';
import Spinner from '../../ui/Spinner';
import CategoryPickerItem from './CategoryPickerItem';
import { useCategories } from './useCategories';

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

function CategoryPicker({
  categories,
  allowCreateCategory,
  selectedCategoryIds,
  setSelectedCategoryIds
}) {
  const toggleCategory = id => {
    if (selectedCategoryIds.includes(id)) {
      setSelectedCategoryIds(selectedCategoryIds.filter(c => c !== id));
    } else {
      setSelectedCategoryIds([...selectedCategoryIds, id]);
    }
  };

  return (
    <StyledPickerContainer>
      <StyledPickerTitle>Categories:</StyledPickerTitle>

      <StyledItemContainer>
        {categories.data.map(c => (
          <CategoryPickerItem
            key={c.id}
            category={c.name}
            handleClick={() => toggleCategory(c.id)}
            isSelected={selectedCategoryIds.includes(c.id)}
          />
        ))}
      </StyledItemContainer>
    </StyledPickerContainer>
  );
}

export default CategoryPicker;

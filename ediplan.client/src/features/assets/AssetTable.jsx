import styled from 'styled-components';
import { useAssets } from './useAssets';
import Spinner from '../../ui/Spinner';
import Empty from '../../ui/Empty';
import Menus from '../../ui/Menus';
import TanstackTable from '../../ui/TanstackTable';
import { useOutletContext } from 'react-router-dom';

const Container = styled.div`
  display: flex;
  flex-direction: column;
  gap: 20px;
`;

function AssetTable() {
  const { category, equipmentColumnDefinitions } = useOutletContext();
  const { assets, error, isPending } = useAssets(category);
  console.log(assets);

  if (error) return <div>{error}</div>;
  if (isPending) return <Spinner />;

  const data = assets.data;

  if (!data.length) return <Empty resource="assets" />;

  return (
    <Container>
      <Menus>
        <TanstackTable
          data={assets}
          columnDefinitions={equipmentColumnDefinitions}>
          <TanstackTable.Wrapper>
            <TanstackTable.Header />
            <TanstackTable.Body />
          </TanstackTable.Wrapper>
          {/* <TanstackTable.Pagination /> */}
        </TanstackTable>
      </Menus>
    </Container>
  );
}

export default AssetTable;

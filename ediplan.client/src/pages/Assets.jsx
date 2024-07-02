import {
  Outlet,
  redirect,
  useLocation,
  useNavigate,
  useParams,
} from 'react-router-dom';

import styled from 'styled-components';

import Tab from '../ui/Tab';
import TabContainer from '../ui/TabContainer';
import Toolbar from '../ui/Toolbar';
import AddAsset from '../features/assets/AddAsset';
import { useEffect } from 'react';

import { equipmentColumnDefinitions } from '../features/assets/equipmentColumnDefinitions';

const Container = styled.div`
  display: flex;
  flex-direction: column;
  gap: 20px;
`;

function Assets() {
  const navigate = useNavigate();
  const { category } = useParams();
  console.log('Assets.jsx render. category: ', category);

  useEffect(() => {
    if (!category) {
      navigate('equipment');
    }
  }, [category, navigate]);

  return (
    <Container>
      <div>
        <TabContainer>
          <Tab route="./equipment">Assets</Tab>
          <Tab route="./rooms">Rooms</Tab>
          <Tab route="./personel">People</Tab>
        </TabContainer>
        <Toolbar>
          <Toolbar.Panel side="left">
            <AddAsset category={category} />
          </Toolbar.Panel>
          <Toolbar.Panel side="right"></Toolbar.Panel>
        </Toolbar>
        {/* This renders an AssetTable componenet on each route */}
        <Outlet context={{ category, equipmentColumnDefinitions }} />
      </div>
    </Container>
  );
}

export default Assets;

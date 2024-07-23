import axios from 'axios';
import qs from 'qs';

// PUT INTO ENVIRONMENT VARIABLE
const BASE_URL = 'https://localhost:7080';

const api = axios.create({
  baseURL: BASE_URL,
  withCredentials: true
});

export async function fetchAssets(queryKey) {
  const query = qs.stringify(queryKey[2]);
  console.log(`Query: ${query}`);

  try {
    const response = await api.get(`api/assets?${query}`);

    // const linkQueries = response.data.links.map((link) => {
    //   link.split('?')[1];
    // });

    return {
      data: response.data.value,
      links: response.data.links,
      headers: response.headers
    };
  } catch (err) {
    console.error(`Error fetching assets: ${err}`);
    throw err;
  }

  // try {
  //   const res = url
  //     ? await api.get(url)
  //     : await api.get(category ? `api/assets/${category}` : 'api/assets');

  //   console.log('Response: ', res);

  //   return {
  //     data: res.data.value,
  //     links: res.data.links,
  //     headers: res.headers,
  //   };
  // } catch (err) {
  //   console.error(`Error fetching assets: ${err}`);
  //   throw err;
  // }
}

export async function deleteAsset(id) {
  try {
    const res = await api.delete(`api/assets/${id}`);
    return res.data;
  } catch (err) {
    console.error('Error deleting data:', err);
    throw new Error(`Error deleting asset ${id}`);
  }
}

export async function createEditAsset(data, id, category = '') {
  const newData = { category, ...data };
  if (!id) {
    console.log(
      `assetsApi: Sending PUT request to: api/assets/${id} :`,
      category,
      newData
    );
    try {
      // const res = await api.put(`api/assets/${category}`, data);
      const res = await api.put(`api/assets`, newData);
      return res.newData;
    } catch (err) {
      console.error(`Error adding ${category}: ${err}`);
      throw new Error(`Error adding ${category}`);
    }
  }
  if (id) {
    try {
      console.log(
        `assetsApi: Sending PATCH request to: api/assets/${id} :`,
        category,
        newData
      );
      const res = await api.patch(`api/assets/${id}`, newData);
      return res.data;
    } catch (err) {
      console.error(`Error updating ${data.name || data.model}: ${err}`);
      throw new Error(`Error updating ${data.name || data.model}`);
    }
  }
}

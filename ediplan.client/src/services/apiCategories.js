import axios from 'axios';
import qs from 'qs';

const BASE_URL = 'https://localhost:7080';

const api = axios.create({
  baseURL: BASE_URL,
  withCredentials: true
});

export async function getCategories() {
  // const query = qs.stringify(queryKey[1]);

  try {
    const response = await api.get(`api/bookings/groups`);
    console.log('Response: ', response);

    // const linkQueries = response.data.links.map((link) => {
    //   link.split('?')[1];
    // });

    return {
      data: response.data
    };
  } catch (err) {
    console.error(`Error fetching assets: ${err}`);
    throw err;
  }
}

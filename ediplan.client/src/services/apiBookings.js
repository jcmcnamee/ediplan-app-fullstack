import axios from 'axios';

const BASE_URL = 'https://localhost:7080';

const api = axios.create({
  baseURL: BASE_URL,
});

export async function getBookings({ searchParams }) {
  try {
    let url = 'api/bookings';

    if (searchParams) {
      url += `?${searchParams}`;
    }

    const res = await api.get(url);
    return res.data;
  } catch (err) {
    console.error(`Error fetching bookings: ${err}`);
  }
}

export async function getBooking({ queryKey }) {}

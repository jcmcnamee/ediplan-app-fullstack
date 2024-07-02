import { useQuery } from '@tanstack/react-query';
import { getBookings } from '../../services/apiBookings';
import { useSearchParams } from 'react-router-dom';

export function useBookings() {
  const [searchParams] = useSearchParams();
  const searchParamsString = searchParams.toString();

  const {
    data: bookings,
    error,
    isPending,
  } = useQuery({
    queryKey: ['bookings', searchParamsString],
    queryFn: () => getBookings({ searchParams }),
  });

  return { bookings, error, isPending };
}

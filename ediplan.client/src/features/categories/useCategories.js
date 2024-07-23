import { useQuery } from '@tanstack/react-query';
import { getCategories } from '../../services/apiCategories';

export function useCategories() {
  const {
    data: categories,
    error,
    isPending,
    isFetching
  } = useQuery({
    queryKey: ['categories'],
    queryFn: getCategories
  });

  return {
    categories,
    error,
    isPending,
    isFetching
  };
}

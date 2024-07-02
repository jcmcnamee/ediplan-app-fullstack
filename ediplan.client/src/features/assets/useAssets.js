import { useQuery, useQueryClient } from '@tanstack/react-query';
import { fetchAssets } from '../../services/apiAssets';
import { useState } from 'react';

// export function useAssets(category) {
//   const { data, error, isPending } = useQuery({
//     queryKey: ['assets', category],
//     queryFn: fetchAssets,
//   });

//   const assets = data.data.value;
//   const links = data.data.links;
//   const paginationHeaderData = JSON.parse(data.headers['x-pagination']);

//   console.log('header data: ', paginationHeaderData);

//   return { assets, links, paginationHeaderData, error, isPending };
// }

export function useAssets(initCategory = null, initUrl = null) {
  const queryClient = useQueryClient();
  const [queryKey, setQueryKey] = useState(['assets', initCategory, initUrl]);

  const { data, error, isPending, isFetching } = useQuery({
    queryKey: queryKey,
    queryFn: fetchAssets,
    keepPreviousData: true,
  });

  const assets = data?.data.value || [];
  const links = data?.data.links || [];
  const paginationHeaderData = data?.headers['x-pagination']
    ? JSON.parse(data.headers['x-pagination'])
    : {};

  const getLink = rel => links.find(link => link.rel === rel);

  const fetchPage = rel => {
    const link = getLink(rel);
    if (link) {
      setQueryKey(['assets', initCategory, link.href]);
    }
  };

  return {
    assets,
    links,
    paginationHeaderData,
    error,
    isPending,
    isFetching,
    getLink,
    fetchPage,
  };
}

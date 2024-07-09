import { useQuery } from '@tanstack/react-query';
import { useState } from 'react';
import { fetchAssets } from '../../services/apiAssets';

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

export function useAssets(filterState) {

  const { data, error, isPending, isFetching } = useQuery({
    queryKey: filterState,
    queryFn: () => fetchAssets(filterState),
    keepPreviousData: true,
  });

  const assets = data?.data || [];
  const links = data?.links || [];
  const paginationHeaderData = data?.headers['x-pagination']
    ? JSON.parse(data.headers['x-pagination'])
    : {};

  // const getLink = (rel) => links.find((link) => link.rel === rel);

  // const fetchPage = (rel) => {
  //   const link = getLink(rel);
  //   if (link) {
  //     setQueryKey(['assets', category, link.href]);
  //   }
  // };

  return {
    assets,
    links,
    paginationHeaderData,
    error,
    isPending,
    isFetching,
  };
}

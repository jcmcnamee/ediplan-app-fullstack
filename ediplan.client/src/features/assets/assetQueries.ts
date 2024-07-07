export const assetKeys = {
  all: ['assets', null] as const,
  equipment: () => [...assetKeys.all, 'equipment'] as const,
  people: () => [...assetKeys.all, 'people'] as const,
  rooms: () => [...assetKeys.all, 'rooms'] as const,
  filter: (tableParams: AssetTableParams) =>
    [...assetKeys.all, tableParams] as const,
  equipmentParams: (filters: string) =>
    [...assetKeys.equipment(), ...filters] as const,
};

export interface AssetTableParams {
  type?: string;
  from?: Date;
  to?: Date;
  sortBy?: string;
  search?: string;
}

// export interface Filter {
//   name: string;
//   value: unknown;
// }

// export interface FilterRanged extends Filter {
//   value: {
//     from: unknown;
//     to: unknown;
//   };

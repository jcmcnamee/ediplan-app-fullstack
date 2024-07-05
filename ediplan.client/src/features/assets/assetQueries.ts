export const assetKeys = {
  all: ['assets'] as const,
  filtered: (filters: AssetFilters) => [...assetKeys.all, { filters }] as const,
  equipment: () => [...assetKeys.all, 'equipment'] as const,
  people: () => [...assetKeys.all, 'people'] as const,
  rooms: () => [...assetKeys.all, 'rooms'] as const,
  equipmentParams: (filters: string) =>
    [...assetKeys.equipment(), { filters }] as const,
};

export interface AssetFilters {
  type?: string;
  available?: {
    from?: Date;
    to?: Date;
    rate?: number;
  };
}

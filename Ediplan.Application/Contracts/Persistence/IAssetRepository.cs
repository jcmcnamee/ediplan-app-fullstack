using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ediplan.Application.Features.Assets.Queries.GetAssetsList;
using Ediplan.Application.Features.Bookings.Queries.GetBookingsList;
using Ediplan.Application.Helpers;
using Ediplan.Domain.Common;
using Ediplan.Domain.Entities;

namespace Ediplan.Application.Contracts.Persistence;

public interface IAssetRepository : IAsyncRepository<Asset>
{
    Task<List<Asset>> GetAssetsByIdsAsync(IEnumerable<int> assetIds);
    Task<PagedList<Asset>> ListAllAsync(GetAssetsListQuery assetResourceParams);
    Task<List<Asset>> GetAvailableAssetsById(IEnumerable<int> assetIds, DateTime startDate, DateTime date);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetsList;
using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;
using EdiplanDotnetAPI.Application.Helpers;
using EdiplanDotnetAPI.Domain.Common;
using EdiplanDotnetAPI.Domain.Entities;

namespace EdiplanDotnetAPI.Application.Contracts.Persistence;

public interface IAssetRepository : IAsyncRepository<Asset>
{
    Task<PagedList<Asset>> ListAllAsync(GetAssetsListQuery assetResourceParams);
}

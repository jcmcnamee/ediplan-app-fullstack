using Ediplan.Application.Contracts.Persistence;
using Ediplan.Application.Features.Assets.Queries.GetAssetsList;
using Ediplan.Application.Helpers;
using Ediplan.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ediplan.Persistence.Repositories;
public class AssetRepository : BaseRepository<Asset>, IAssetRepository
{
    private readonly EdiplanDbContext _dbContext;
    private readonly ILogger<AssetRepository> _logger;

    public AssetRepository(EdiplanDbContext dbContext, ILogger<AssetRepository> logger) : base(dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<PagedList<Asset>> ListAllAsync(GetAssetsListQuery assetResourceParams)
    {
        if (assetResourceParams == null)
        {
            throw new ArgumentException(nameof(assetResourceParams));
        }

        // Collection to start from
        var collection = _dbContext.Assets as IQueryable<Asset>;

        // Filter
        if(!string.IsNullOrWhiteSpace(assetResourceParams.Type))
        {
            collection = collection.Where(a => a.Type == assetResourceParams.Type);
        }

        if (assetResourceParams.From.HasValue && assetResourceParams.To.HasValue)
        {
            collection = ExcludeAssetsWithBookings(assetResourceParams.From.Value, assetResourceParams.To.Value, collection);
        }

        // Search

        // Sort

        // Page
        int count = collection.Count();
        var source = await collection.Skip((assetResourceParams.Page - 1) * assetResourceParams.PageSize)
            .Take(assetResourceParams.PageSize)
            .ToListAsync();

        return new PagedList<Asset>(source, count, assetResourceParams.Page, assetResourceParams.PageSize);

        //return await PagedList<Asset>.CreateAsync(collection, assetResourceParams.Page, assetResourceParams.PageSize);
    }

    private static IQueryable<Asset> ExcludeAssetsWithBookings(DateTime startDate, DateTime endDate, IQueryable<Asset> collection)
    {
        collection = collection.Where(a => !a.Bookings.Any(b =>
                        (endDate >= b.StartDate && endDate <= b.EndDate) ||
                        (startDate >= b.StartDate && startDate <= b.EndDate) ||
                        (startDate <= b.StartDate && endDate >= b.EndDate)
                        ));

        return collection;
    }

    public async Task<List<Asset>> GetAssetsByIdsAsync(IEnumerable<int> assetIds)
    {
        return await _dbContext.Assets.Where(a => assetIds.Contains(a.Id)).ToListAsync();
    }

    public async Task<List<Asset>> GetAvailableAssetsById(IEnumerable<int> assetIds, DateTime startDate, DateTime date)
    {
        var collection = _dbContext.Assets as IQueryable<Asset>;
        collection = collection.Where(a => assetIds.Contains(a.Id));
        collection = ExcludeAssetsWithBookings(startDate, date, collection);
        return await collection.ToListAsync();
    }

}

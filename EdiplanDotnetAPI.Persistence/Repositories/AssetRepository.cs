using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetsList;
using EdiplanDotnetAPI.Application.Helpers;
using EdiplanDotnetAPI.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EdiplanDotnetAPI.Persistence.Repositories;
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

        if(!string.IsNullOrWhiteSpace(assetResourceParams.Type))
        {
            collection = collection.Where(a => a.Type == assetResourceParams.Type);
        }

        // Sort

        // Page
        int count = collection.Count();
        var source = await collection.Skip((assetResourceParams.Page - 1) * assetResourceParams.PageSize)
            .Take(assetResourceParams.PageSize)
            .ToListAsync();

        return new PagedList<Asset>(source, count, assetResourceParams.Page, assetResourceParams.PageSize);

        //return await PagedList<Asset>.CreateAsync(collection, assetResourceParams.Page, assetResourceParams.PageSize);
    }


}

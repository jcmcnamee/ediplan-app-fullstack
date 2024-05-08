using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Domain.Common;

namespace EdiplanDotnetAPI.Persistence.Repositories;
public class AssetRepository : BaseRepository<Asset>, IAssetRepository
{
    private readonly EdiplanDbContext _dbContext;

    public AssetRepository(EdiplanDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


}

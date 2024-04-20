using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdiplanDotnetAPI.Domain.Common;

namespace EdiplanDotnetAPI.Application.Contracts.Persistence;

public interface IAssetRepository : IAsyncRepository<Asset>
{
    
}

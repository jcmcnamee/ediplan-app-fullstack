using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdiplanDotnetAPI.Domain.Entities;

namespace EdiplanDotnetAPI.Application.Contracts.Persistence;

public interface IRoomRepository : IAsyncRepository<Room>
{
    
}

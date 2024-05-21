using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Persistence.Repositories;
internal class EquipmentRepository : BaseRepository<Equipment>, IEquipmentRepository
{
    private readonly EdiplanDbContext _dbContext;

    public EquipmentRepository(EdiplanDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}

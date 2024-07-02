using Ediplan.Application.Contracts.Persistence;
using Ediplan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Persistence.Repositories;
public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    private readonly EdiplanDbContext _dbContext;

    public PersonRepository(EdiplanDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}

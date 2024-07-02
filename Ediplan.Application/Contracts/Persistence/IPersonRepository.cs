using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ediplan.Domain.Entities;

namespace Ediplan.Application.Contracts.Persistence;

public interface IPersonRepository : IAsyncRepository<Person>
{
    
}

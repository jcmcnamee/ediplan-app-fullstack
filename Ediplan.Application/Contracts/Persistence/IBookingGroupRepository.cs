using Ediplan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Contracts.Persistence;
public interface IBookingGroupRepository : IAsyncRepository<BookingGroup>
{
    Task<List<BookingGroup>> GetBookingGroupsWithMembers(bool includePastEvents);
}

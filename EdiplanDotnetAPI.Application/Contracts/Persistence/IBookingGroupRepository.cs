using EdiplanDotnetAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Contracts.Persistence;
public interface IBookingGroupRepository : IAsyncRepository<BookingGroup>
{
    Task<List<BookingGroup>> GetBookingGroupsWithMembers(bool includePastEvents);
}

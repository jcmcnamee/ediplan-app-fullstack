using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ediplan.Application.Features.Bookings.Queries.GetBookingsList;
using Ediplan.Application.Helpers;
using Ediplan.Domain.Entities;

namespace Ediplan.Application.Contracts.Persistence;

public interface IBookingRepository : IAsyncRepository<Booking>
{
    Task<IReadOnlyList<Booking>> ListAllAsync(bool includeNavProps);
    Task<PagedList<Booking>> ListAllAsync(GetBookingsListQuery bookingResourceParams);
    Task<bool> IsBookingNameAndDateUnique(string name, DateTime bookingDate);
}

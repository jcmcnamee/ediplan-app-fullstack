using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;
using EdiplanDotnetAPI.Application.Helpers;
using EdiplanDotnetAPI.Domain.Entities;

namespace EdiplanDotnetAPI.Application.Contracts.Persistence;

public interface IBookingRepository : IAsyncRepository<Booking>
{
    Task<IReadOnlyList<Booking>> ListAllAsync(bool includeNavProps);
    Task<PagedList<Booking>> ListAllAsync(GetBookingsListQuery bookingResourceParams);
    Task<bool> IsBookingNameAndDateUnique(string name, DateTime bookingDate);
}

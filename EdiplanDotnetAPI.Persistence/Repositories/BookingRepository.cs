using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;
using EdiplanDotnetAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EdiplanDotnetAPI.Persistence.Repositories;

public class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    private readonly EdiplanDbContext _dbContext;
    public BookingRepository(EdiplanDbContext dbContext) : base(dbContext)
    {
            _dbContext = dbContext;
        
    }
    public Task<bool> IsBookingNameAndDateUnique(string name, DateTime bookingDate)
    {
        var matches = _dbContext.Bookings.Any(b => b.Name.Equals(name) &&
        b.StartDate.Date.Equals(bookingDate.Date));

        return Task.FromResult(matches);
    }

    public async Task<IReadOnlyList<Booking>> ListAllAsync(bool includeNavProps)
    {
        if(includeNavProps)
        {
            return await _dbContext.Bookings
                .Include(b => b.Production)
                .Include(b => b.Location)
                .ToListAsync();
        }

        return await _dbContext.Bookings.ToListAsync();
    }

    public async Task<IReadOnlyList<Booking>> ListAllAsync(GetBookingsListQuery bookingResourceParams)
    {
        if (bookingResourceParams == null)
        {
            throw new ArgumentException(nameof(bookingResourceParams));
        }

        if (string.IsNullOrWhiteSpace(bookingResourceParams.Status) && string.IsNullOrWhiteSpace(bookingResourceParams.SearchQuery))
        {
            return await _dbContext.Bookings
                .Include(b => b.Production)
                .Include(b => b.Location)
                .ToListAsync();
        }


        var collection = _dbContext.Bookings
            .Include(b => b.Production)
            .Include(b => b.Location)
            as IQueryable<Booking>;

        if (!string.IsNullOrWhiteSpace(bookingResourceParams.Status))
        {
            collection = collection.Where(b => b.Status == bookingResourceParams.Status);
        }

        return await collection.ToListAsync();
    }
}

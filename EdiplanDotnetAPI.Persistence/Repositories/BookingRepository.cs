using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Domain.Entities;

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
}

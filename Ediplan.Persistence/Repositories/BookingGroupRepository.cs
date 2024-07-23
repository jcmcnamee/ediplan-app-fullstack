using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ediplan.Application.Contracts.Persistence;
using Ediplan.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ediplan.Persistence.Repositories;

public class BookingGroupRepository : BaseRepository<BookingGroup>, IBookingGroupRepository
{
        private readonly EdiplanDbContext _dbContext;
    public BookingGroupRepository(EdiplanDbContext dbContext) : base(dbContext)
    {
            _dbContext = dbContext;
        
    }

    // This method is used to get a list of groups with their members
    public async Task<List<BookingGroup>> GetBookingGroupsWithMembers(bool includePastEvents)
    {
        var allGroups = await _dbContext.BookingGroups.Include(x => 
        x.Bookings).ToListAsync();

        if (!includePastEvents)
        {
            allGroups.ForEach(p => p.Bookings.ToList().RemoveAll(c => c.StartDate < DateTime.Today));
        }

        return allGroups;
    }

    // This method is used to get a list of groups by their Ids
    public Task<List<BookingGroup>> GetGroupsByIdsAsync(IEnumerable<int> groupIds)
    {
        return _dbContext.BookingGroups.Where(g => groupIds.Contains(g.Id)).ToListAsync();
    }
}

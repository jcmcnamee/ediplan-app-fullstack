using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EdiplanDotnetAPI.Persistence.Repositories;

public class BookingGroupRepository : BaseRepository<BookingGroup>, IBookingGroupRepository
{
        private readonly EdiplanDbContext _dbContext;
    public BookingGroupRepository(EdiplanDbContext dbContext) : base(dbContext)
    {
            _dbContext = dbContext;
        
    }

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

}

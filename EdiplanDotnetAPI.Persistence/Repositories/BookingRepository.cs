using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;
using EdiplanDotnetAPI.Application.Helpers;
using EdiplanDotnetAPI.Application.Services;
using EdiplanDotnetAPI.Domain.Common;
using EdiplanDotnetAPI.Domain.Entities;
using EdiplanDotnetAPI.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EdiplanDotnetAPI.Persistence.Repositories;

public class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    private readonly EdiplanDbContext _dbContext;
    private readonly IPropertyMappingService _propertyMappingService;
    private readonly IMapper _mapper;

    public BookingRepository(EdiplanDbContext dbContext, IPropertyMappingService propertyMappingService, IMapper mapper) : base(dbContext)
    {
            _dbContext = dbContext;
            _propertyMappingService = propertyMappingService;
            _mapper = mapper;
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

    public async Task<PagedList<Booking>> ListAllAsync(GetBookingsListQuery bookingResourceParams)
    {
        if (bookingResourceParams == null)
        {
            throw new ArgumentException(nameof(bookingResourceParams));
        }

        //if (string.IsNullOrWhiteSpace(bookingResourceParams.Status) && string.IsNullOrWhiteSpace(bookingResourceParams.Search))
        //{
        //    return await _dbContext.Bookings
        //        .Include(b => b.Production)
        //        .Include(b => b.Location)
        //        .ToListAsync();
        //}

        var collection = _dbContext.Bookings
            .Include(b => b.Production)
            .Include(b => b.Location)
            as IQueryable<Booking>;

        // Filter
        if (!string.IsNullOrWhiteSpace(bookingResourceParams.Status))
        {
            collection = collection.Where(b => b.Status == bookingResourceParams.Status);
        }

        // Search

        // Sort
        if(!string.IsNullOrWhiteSpace(bookingResourceParams.OrderBy))
        {
            var bookingPropertyMappingDictionary = _propertyMappingService
                .GetPropertyMapping<BookingListVm, Booking>();

            collection = collection.ApplySort(bookingResourceParams.OrderBy,  bookingPropertyMappingDictionary);
        }

        // Page
        int count = collection.Count();
        var source = await collection.Skip((bookingResourceParams.Page - 1) * bookingResourceParams.PageSize)
            .Take(bookingResourceParams.PageSize)
            .ToListAsync();

        return new PagedList<Booking>(source, count, bookingResourceParams.Page, bookingResourceParams.PageSize);

    }
}

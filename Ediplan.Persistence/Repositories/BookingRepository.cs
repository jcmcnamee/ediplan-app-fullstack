using AutoMapper;
using Ediplan.Application.Contracts.Persistence;
using Ediplan.Application.Features.Bookings.Queries.GetBookingsList;
using Ediplan.Application.Helpers;
using Ediplan.Application.Services;
using Ediplan.Domain.Common;
using Ediplan.Domain.Entities;
using Ediplan.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Ediplan.Persistence.Repositories;

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

    public async Task<IReadOnlyList<Booking>> ListAllAsync(bool includeNavProps) // DEPRECIATED?
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

    public async Task<PagedList<Booking>> ListAllAsync(GetBookingsListQuery queryParams)
    {
        if (queryParams == null)
        {
            throw new ArgumentException(nameof(queryParams));
        }

        // Collection as IQueryable as it allows us to create an expression tree
        // before execution.
        var collection = _dbContext.Bookings
            .Include(b => b.Production)
            .Include(b => b.Location)
            as IQueryable<Booking>;

        // Filters
        if (!string.IsNullOrWhiteSpace(queryParams.Status))
        {
            collection = collection.Where(b => b.Status == queryParams.Status);
        }

        // Search
        if (!string.IsNullOrWhiteSpace(queryParams.Search))
        {
            var searchQuery = queryParams.Search.Trim();

            collection = collection.Where(b => b.Name == searchQuery
                || b.Production.Name == searchQuery
                || b.Status == searchQuery
                || b.Notes == searchQuery
                || b.Location.Name == searchQuery);
        }

        // Sort
        if (!string.IsNullOrWhiteSpace(queryParams.SortBy))
        {
            var bookingPropertyMappingDictionary = _propertyMappingService
                .GetPropertyMapping<BookingListVm, Booking>();

            collection = collection.ApplySort(queryParams.SortBy, bookingPropertyMappingDictionary);
        }

        // Page
        int count = collection.Count();
        var source = await collection.Skip((queryParams.Page - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .ToListAsync();

        return new PagedList<Booking>(source, count, queryParams.Page, queryParams.PageSize);

    }

    public async Task<Booking> GetBookingDetail(int id)
    {
        var entity = await _dbContext.Bookings.Include(b => b.Assets).FirstOrDefaultAsync(b => b.Id == id);
        return entity;
            
    }
}

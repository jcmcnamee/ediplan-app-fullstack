using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;
using EdiplanDotnetAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Services;

/// <summary>
/// Contains a list of property mappings
/// </summary>
public class PropertyMappingService : IPropertyMappingService
{
    private readonly Dictionary<string, PropertyMappingValue> _bookingPropertyMapping =
        new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
    {
            { "Name", new(new[] { "Name" }) },
            { "StartDate", new(new[] { "StartDate" }) },
            { "EndDate", new(new[] { "EndDate" }) },
            { "Status", new(new[] { "Status" }) },
    };

    private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

    public PropertyMappingService()
    {
        _propertyMappings.Add(new PropertyMapping<BookingListVm, Booking>(
            _bookingPropertyMapping));
    }


    // Gets single property mapping
    public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestionation>()
    {

        var matchMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestionation>>();

        if (matchMapping.Count() == 1)
        {
            return matchMapping.First().MappingDictionary;
        }

        throw new Exception($"Cannot find exact property mapping instance for <{typeof(TSource)}, {typeof(TDestionation)}>.");
    }

}

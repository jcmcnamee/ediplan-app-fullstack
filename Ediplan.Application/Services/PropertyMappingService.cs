using Ediplan.Application.Features.Assets.Queries.GetAssetsList;
using Ediplan.Application.Features.Bookings.Queries.GetBookingsList;
using Ediplan.Domain.Common;
using Ediplan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Services;

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
            { "Status", new(new[] { "Status" }) }
    };

    private readonly Dictionary<string, PropertyMappingValue> _assetPropertyMapping =
        new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
    {
        { "Name", new(new[] {"Name"}) },
        { "CreatedDate", new(new[] {"CreatedDate"}) }
    };

    private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

    public PropertyMappingService()
    {
        _propertyMappings.Add(new PropertyMapping<BookingListVm, Booking>(
            _bookingPropertyMapping));
        _propertyMappings.Add(new PropertyMapping<AssetListVm, Asset>(
            _assetPropertyMapping));
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

    public bool ValidMappingExistsFor<TSource, TDestionation>(string fields)
    {
        var propertyMapping = GetPropertyMapping<TSource, TDestionation>();

        if (string.IsNullOrWhiteSpace(fields))
        {
            return true;
        }

        var splitFields = fields.Split(',');

        foreach (var field in splitFields)
        {
            var trimmedField = field.Trim();

            // Remove everything after the first " " and ignore sortBy string
            var indexOfFirstSpace = trimmedField.IndexOf(' ');
            var propertyName = indexOfFirstSpace == -1 ?
                trimmedField : trimmedField.Remove(indexOfFirstSpace);

            // Find matching property
            if (!propertyMapping.ContainsKey(propertyName))
            {
                return false;
            }

        }
        return true;
    }

}

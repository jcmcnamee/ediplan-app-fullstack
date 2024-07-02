using Ediplan.Application.Services;
using System.Linq.Dynamic.Core;

namespace Ediplan.Persistence.Helpers;
public static class IQueryableExtensions
{
    public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string sortBy, Dictionary<string,
        PropertyMappingValue> mappingDictionary)
    {
        if(source == null)
            throw new ArgumentNullException(nameof(source));

        if(mappingDictionary == null)
            throw new ArgumentNullException(nameof(mappingDictionary));

        if(string.IsNullOrWhiteSpace(sortBy))
            return source;

        var sortByString = string.Empty;

        var sortBySplit = sortBy.Split(',');

        foreach (var sortByClause in sortBySplit)
        {
            var trimmedClause = sortByClause.Trim();

            // If clause ends in " desc", we order descending otherwise ascensding.
            var orderDescending = trimmedClause.EndsWith(" desc");

            // Remove " asc" or " desc" from clause to get property name for dictionary look up.
            var indexOfFirstSpace = trimmedClause.IndexOf(" ");
            var propertyName = indexOfFirstSpace == -1 ? trimmedClause : trimmedClause.Remove(indexOfFirstSpace);

            // Find matching property
            if (!mappingDictionary.ContainsKey(propertyName))
            {
                throw new ArgumentException($"Key mapping for {propertyName} is missing.");
            }

            // Get the PropertyMappingValue
            var propertyMappingValue = mappingDictionary[propertyName];

            if(propertyMappingValue == null)
            {
                throw new ArgumentNullException(nameof(propertyMappingValue));
            }

            // Revert sort order if necessary
            if (propertyMappingValue.Revert)
            {
                orderDescending = !orderDescending;
            }

            // Loop through the property names
            foreach (var destinationProperty in propertyMappingValue.DestinationProperties)
            {
                sortByString = sortByString + (string.IsNullOrWhiteSpace(sortByString) ? string.Empty : ", ")
                    + destinationProperty
                    + (orderDescending ? " desc" : " asc");
            }
        }
        return source.OrderBy(sortByString);
    }
}

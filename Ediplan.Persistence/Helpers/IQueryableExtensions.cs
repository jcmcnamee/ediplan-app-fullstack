using Ediplan.Application.Services;
using System.Linq.Dynamic.Core;

namespace Ediplan.Persistence.Helpers;
public static class IQueryableExtensions
{
    /// <summary>
    /// Applies sorting to the IQueryable based on the provided sortBy string and mapping dictionary.
    /// </summary>
    /// <typeparam name="T">The type of the IQueryable.</typeparam>
    /// <param name="source">The IQueryable to apply sorting to.</param>
    /// <param name="sortBy">The string specifying the sorting criteria.</param>
    /// <param name="mappingDictionary">The dictionary mapping property names to their corresponding sorting values.</param>
    /// <returns>The IQueryable with sorting applied.</returns>
    /// <exception cref="ArgumentNullException">Thrown when source or mappingDictionary is null.</exception>
    /// <exception cref="ArgumentException">Thrown when a key mapping for a property is missing.</exception>  
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

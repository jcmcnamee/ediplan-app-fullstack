using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Services;

/// <summary>
/// Maps from source to destination
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TDestionation"></typeparam>
public class PropertyMapping<TSource, TDestionation> : IPropertyMapping
{
    public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
    {
        MappingDictionary = mappingDictionary
            ?? throw new ArgumentNullException(nameof(mappingDictionary));
    }

    public Dictionary<string, PropertyMappingValue> MappingDictionary { get; private set; }
}

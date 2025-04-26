using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Helpers;
public static class ObjectExtensions
{
    public static ExpandoObject ShapeData<TSource>(this TSource source, string? fields)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var shapedObject = new ExpandoObject();

        if (string.IsNullOrEmpty(fields))
        {
            // All public properties should be included
            var propertyInfos = typeof(TSource)
                .GetProperties(
                BindingFlags.IgnoreCase
                | BindingFlags.Public
                | BindingFlags.Instance);

            foreach (var propertyInfo in propertyInfos)
            {
                // Get the value of the property on the souece
                var propertyValue = propertyInfo.GetValue(source);

                // Add to ExpandoObject
                ((IDictionary<string, object?>)shapedObject).Add(propertyInfo.Name, propertyValue);
            }

            return shapedObject;

        }

        // If fields are contained in params
        var splitFields = fields.Split(',');

        foreach (var field in splitFields)
        {
            var propertyname = field.Trim();

            // Get property
            var propertyInfo = typeof(TSource)
                .GetProperty(propertyname,
                BindingFlags.IgnoreCase
                | BindingFlags.Public
                | BindingFlags.Instance);

            if (propertyInfo == null)
            {
                throw new Exception($"Property {propertyname} wasn't found on {typeof(TSource)}");
            }

            // Get the value of the property on the source
            var propertyValue = propertyInfo.GetValue(source);

            // Add to ExpandoObject
            ((IDictionary<string, object?>)shapedObject).Add(propertyInfo.Name, propertyValue);
        }

        return shapedObject;

    }
}

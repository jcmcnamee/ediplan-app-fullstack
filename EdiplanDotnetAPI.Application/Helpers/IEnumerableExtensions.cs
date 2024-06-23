using System.Dynamic;
using System.Reflection;


namespace EdiplanDotnetAPI.Application.Helpers;
public static class IEnumerableExtensions
{
    public static IEnumerable<ExpandoObject> ShapeData<TSource>(this IEnumerable<TSource> source, string? fields)
    {
        if (source == null) throw new ArgumentNullException();

        // Create a list to hold our ExpandoObjects
        var expandoObjectList = new List<ExpandoObject>();

        // Create a list with PropertyInfo objects on TSource.
        // We do this once rather than on each object due to reflection expenses...
        var propertyInfoList = new List<PropertyInfo>();

        // If no fields are passed we use all properties, otherwise we get just the ones specified
        if (string.IsNullOrWhiteSpace(fields))
        {
            // Get all public and instance properties ignoring case sensitivity.
            var propertyInfo = typeof(TSource).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            // Add all properties to the list
            propertyInfoList.AddRange(propertyInfo);
        }
        else
        {
            var splitFields = fields.Split(',');

            foreach(var field in splitFields)
            {
                var propertyName = field.Trim();

                // Use reflection to get property on the source object.
                // Include public and instance as specifying binding flags do not persist between calls
                var propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    // MAYBE LOG HERE INSTEAD OF THROWING AN EXCEPTION
                    throw new Exception($"Property {propertyName} wasn't found on {typeof(TSource)}");
                }

                // Add propertyInfo to list
                propertyInfoList.Add(propertyInfo);
            }
        }

        // Loop over all source objects
        foreach(TSource sourceObj in source)
        {
            // Create ExpandoObject
            var shapedObject = new ExpandoObject();

            foreach (var propertyInfo in propertyInfoList)
            {
                // Get the value of the property on the source object
                var propertyValue = propertyInfo.GetValue(sourceObj);

                // Add the field to the ExpandoObject cast as a Dictionary
                ((IDictionary<string, object?>)shapedObject).Add(propertyInfo.Name, propertyValue);
            }

            // Add the ExpandoObject to the list
            expandoObjectList.Add(shapedObject);
        }

        return expandoObjectList;
    }
}

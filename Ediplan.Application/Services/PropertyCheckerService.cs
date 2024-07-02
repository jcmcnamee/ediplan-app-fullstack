using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Services;
public class PropertyCheckerService : IPropertyCheckerService
{
    public bool TypeHasProperties<T>(string? fields)
    {
        if (string.IsNullOrWhiteSpace(fields))
        {
            return true;
        }


        var splitFields = fields.Split(',');

        foreach (var field in splitFields)
        {
            var propertyname = field.Trim();

            // Get property
            var propertyInfo = typeof(T)
                .GetProperty(propertyname,
                BindingFlags.IgnoreCase
                | BindingFlags.Public
                | BindingFlags.Instance);

            // If any property can't be found
            if (propertyInfo == null)
            {
                return false;
            }
        }

        // All exist
        return true;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Services;

/// <summary>
/// 
/// </summary>
public class PropertyMappingValue
{
    // Resource property will map to this
    public IEnumerable<string> DestinationProperties { get; private set; }

    // Allows reverse sort if needed.
    public bool Revert {  get; private set; }

    public PropertyMappingValue(IEnumerable<string> destinationProps, bool revert = false)
    {
        DestinationProperties = destinationProps
            ?? throw new ArgumentNullException(nameof(destinationProps));

        Revert = revert;
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdiplanDotnetAPI.Domain.Common;

namespace EdiplanDotnetAPI.Domain.Entities;

public class AssetGroup
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Navigation properties
    public List<Asset> Assets { get; set; } = new List<Asset>();
    
    public AssetGroup(string name)
    {
        Name = name;
    }
}

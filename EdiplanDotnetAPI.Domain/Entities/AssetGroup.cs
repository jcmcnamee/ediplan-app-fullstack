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

    // Foreign Keys
    public Guid? ParentGroupId { get; set; }

    // Navigation properties
    public AssetGroup? ParentGroup { get; set; }
    public ICollection<Asset> Assets { get; set; } = new List<Asset>();
    
}

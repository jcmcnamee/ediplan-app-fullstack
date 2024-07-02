using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ediplan.Domain.Common;

namespace Ediplan.Domain.Entities;

public class AssetGroup
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Foreign Keys
    public Guid? ParentGroupId { get; set; }

    // Navigation properties
    public AssetGroup? ParentGroup { get; set; }
    public List<Asset> Assets { get; set; } = new();
    
}

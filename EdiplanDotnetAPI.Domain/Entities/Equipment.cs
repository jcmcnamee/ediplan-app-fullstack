using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdiplanDotnetAPI.Domain.Common;

namespace EdiplanDotnetAPI.Domain.Entities;

public class Equipment : Asset
{
    public override string Type { get; set; } = "equipment";
    public string? AssetNumber { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public decimal? Value { get; set; }
    public bool IsLostOrBroken { get; set; }
    public string? Description { get; set; }
}

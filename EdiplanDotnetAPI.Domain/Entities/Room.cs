using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdiplanDotnetAPI.Domain.Common;

namespace EdiplanDotnetAPI.Domain.Entities;

public class Room : Asset
{
    public override string Type { get; set; } = "Room";
    public string? UsedFor { get; set; }
    public string? Description { get; set; }
}

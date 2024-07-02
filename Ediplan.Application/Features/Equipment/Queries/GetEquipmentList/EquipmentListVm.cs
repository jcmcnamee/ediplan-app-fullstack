using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Features.Equipment.Queries.GetEquipmentList;
public class EquipmentListVm
{
    public int? AssetNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Make { get; set; }
    public string? Model { get; set; }
    public decimal? value { get; set; }
    public decimal? Rate { get; set; }
    public decimal? RateUnit { get; set; }
    public string? Description { get; set; }
}

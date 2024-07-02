using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Features.Assets.Queries.GetAssetDetail;
internal class EquipmentDetailVm : AssetDetailVm
{
    public string Type { get; set; } = "equipment";
    public string? AssetNumber { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public decimal? Value { get; set; }
    public string? Description { get; set; }
}

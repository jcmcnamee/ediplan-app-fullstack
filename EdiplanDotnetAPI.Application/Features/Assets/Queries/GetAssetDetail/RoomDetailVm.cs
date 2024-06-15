using EdiplanDotnetAPI.Application.Contracts;

namespace EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetDetail;

internal class RoomDetailVm : AssetDetailVm
{
    public string Type { get; set; } = string.Empty;
    public decimal? Value { get; set; }
    public string? UsedFor { get; set; }
    public string? Description { get; set; }
}
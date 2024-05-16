using EdiplanDotnetAPI.Application.Contracts;

namespace EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetDetail;

internal class RoomDetailVm : IAssetDetailVm
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal? Rate { get; set; }
    public decimal? RateUnit { get; set; }
    public decimal? Value { get; set; }
    public string? UsedFor { get; set; }
    public string? Description { get; set; }
}
namespace Ediplan.Application.Features.Assets.Queries.GetAssetDetail;
public class AssetDetailVm
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal? Rate { get; set; }
    public decimal? RateUnit { get; set; }
}

namespace EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetDetail;

internal class PersonDetailVm : AssetDetailVm
{
    public string Type { get; set; } = string.Empty;
    public decimal? Value { get; set; }
    public string? Role { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool IsStaff { get; set; }
}
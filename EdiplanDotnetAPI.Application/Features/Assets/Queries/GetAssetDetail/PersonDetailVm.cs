using EdiplanDotnetAPI.Application.Contracts;

namespace EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetDetail;

internal class PersonDetailVm : IAssetDetailVm
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal? Rate { get; set; }
    public decimal? RateUnit { get; set; }
    public decimal? Value { get; set; }
    public string? Role { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool IsStaff { get; set; }
}
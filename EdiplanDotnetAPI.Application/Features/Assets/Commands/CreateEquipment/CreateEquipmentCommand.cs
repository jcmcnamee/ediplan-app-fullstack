using EdiplanDotnetAPI.Application.Contracts;

namespace EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateEquipment;
public class CreateEquipmentCommand : ICreateAssetCommand
{
    public string Name { get; set; } = string.Empty;
    public decimal? Rate { get; set; }
    public decimal? RateUnit { get; set; }
    public decimal? Value { get; set; }
    public string? AssetNumber { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string? Description { get; set; }
}

using EdiplanDotnetAPI.Application.Responses;

namespace EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateEquipment;
public class CreateEquipmentCommandResponse : BaseResponse
{
    public CreateEquipmentCommandResponse() : base()
    {

    }

    public CreateEquipmentDto Asset { get; set; } = default!;
}

using Ediplan.Application.Responses;

namespace Ediplan.Application.Features.Assets.Commands.CreateEquipment;
public class CreateEquipmentCommandResponse : BaseResponse
{
    public CreateEquipmentCommandResponse() : base()
    {

    }

    public CreateEquipmentDto Asset { get; set; } = default!;
}

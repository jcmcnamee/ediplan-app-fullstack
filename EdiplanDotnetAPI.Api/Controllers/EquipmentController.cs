using EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateEquipment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EdiplanDotnetAPI.Api.Controllers;

[ApiController]
[Route("api/assets/equipment")]
public class EquipmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public EquipmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    [HttpPost(Name = "CreateEquipment")]
    public async Task<ActionResult<CreateEquipmentCommandResponse>> Create([FromBody]
    CreateEquipmentCommand createEquipmentCommand)
    {
        var response = await _mediator.Send(createEquipmentCommand);
        return CreatedAtRoute("GetAssetById", new {Id = response.Equipment.Id}, response);
    }
}

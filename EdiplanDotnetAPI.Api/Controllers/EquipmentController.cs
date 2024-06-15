using EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateAsset;
using EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateEquipment;
using EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetsList;
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

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    [HttpGet(Name = "GetAllEquipment")]
    public async Task<ActionResult<List<AssetListVm>>> GetAllEquipment()
    {
        var result = await _mediator.Send(new GetAssetsListQuery());

        if (result == null || result.Count == 0)
        {
            return NoContent();
        }

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    [HttpPost(Name = "CreateEquipment")]
    public async Task<ActionResult<CreateAssetCommandResponse>> Create([FromBody] CreateEquipmentCommand createEquipmentCommand)
    {
        var response = await _mediator.Send(createEquipmentCommand);
        return CreatedAtRoute("GetAssetById", new {Id = response.Asset.Id}, response);
    }
}

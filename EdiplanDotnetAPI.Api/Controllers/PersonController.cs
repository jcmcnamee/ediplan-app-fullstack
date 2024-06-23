using EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateEquipment;
using EdiplanDotnetAPI.Application.Features.Assets.Commands.CreatePerson;
using EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetsList;
using EdiplanDotnetAPI.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EdiplanDotnetAPI.Api.Controllers;

[ApiController]
[Route("api/assets/person")]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
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
    [HttpGet(Name = "GetAllPeople")]
    public async Task<ActionResult<List<AssetListVm>>> GetAllPeople()
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
    [HttpPost(Name = "CreatePerson")]
    public async Task<ActionResult<CreateAssetCommandResponse>> Create([FromBody] CreatePersonCommand createPersonCommand)
    {
        var response = await _mediator.Send(createPersonCommand);
        return CreatedAtRoute("GetAssetById", new { Id = response.Asset.Id }, response);
    }
}

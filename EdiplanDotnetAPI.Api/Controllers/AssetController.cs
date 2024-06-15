using EdiplanDotnetAPI.Application.Contracts;
using EdiplanDotnetAPI.Application.Features.Assets.Commands.DeleteAsset;
using EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetDetail;
using EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetsList;
using EdiplanDotnetAPI.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EdiplanDotnetAPI.Api.Controllers;

[ApiController]
[Route("api/assets")]
public class AssetController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetController(IMediator mediator)
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
    [HttpGet(Name = "GetAllAssets")]
    public async Task<ActionResult<List<AssetListVm>>> GetAllAssets()
    {
        var result = await _mediator.Send(new GetAssetsListQuery());

        if(result == null || result.Count == 0)
        {
            return NoContent();
        }

        return Ok(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [HttpGet("{id}", Name = "GetAssetById")]
    public async Task<ActionResult<AssetDetailVm>> GetAssetById(int id)
    {
        var getAssetDetailQuery = new GetAssetDetailQuery()
        {
            Id = id
        };

        return Ok(await _mediator.Send(getAssetDetailQuery));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [HttpDelete("{id}", Name = "DeleteAsset")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleteAssetCommand = new DeleteAssetCommand() { Id = id };
        await _mediator.Send(deleteAssetCommand);
        return NoContent();
    }



}

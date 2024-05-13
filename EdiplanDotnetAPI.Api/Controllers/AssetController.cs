using EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetsList;
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

    [HttpGet(Name = "GetAllAssets")]
    public async Task<ActionResult<List<AssetListVm>>> GetAllAssets()
    {
        var result = await _mediator.Send(new GetAssetsListQuery());

        return Ok(result);
    }
}

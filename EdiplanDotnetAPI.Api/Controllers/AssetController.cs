using EdiplanDotnetAPI.Api.Utility;
using EdiplanDotnetAPI.Application.Features.Assets.Commands.DeleteAsset;
using EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetDetail;
using EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
    [HttpGet(Name = "GetAssets")]
    public async Task<ActionResult<AssetListVm>> GetAssets([FromQuery] GetAssetsListQuery assetResourceParams)
    {
        var result = await _mediator.Send(assetResourceParams);
        if(result == null || result.Count == 0)
        {
            return NoContent();
        }

        var previousPageLink = result.HasPrevious ? CreateAssetsResourceUri(
            assetResourceParams, ResourceUriType.PreviousPage) : null;

        var nextPageLink = result.HasNext ? CreateAssetsResourceUri(
            assetResourceParams, ResourceUriType.NextPage) : null;

        var paginationMetadata = new
        {
            totalCount = result.TotalCount,
            pageSize = result.PageSize,
            currentPage = result.CurrentPage,
            totalPages = result.TotalPages,
            previousPageLink = previousPageLink,
            nextPageLink = nextPageLink
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata)); 

        return Ok(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="assetResourceParams"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private string? CreateAssetsResourceUri(GetAssetsListQuery assetResourceParams, ResourceUriType type)
    {
        switch (type)
        {
            case ResourceUriType.PreviousPage:
                return Url.Link("GetAssets", new
                {
                    orderBy = assetResourceParams.OrderBy,
                    page = assetResourceParams.Page - 1,
                    pageSize = assetResourceParams.PageSize,
                    type = assetResourceParams.Type,
                    search = assetResourceParams.Search
                });
            case ResourceUriType.NextPage:
                return Url.Link("GetAssets", new
                {
                    orderBy = assetResourceParams.OrderBy,
                    page = assetResourceParams.Page + 1,
                    pageSize = assetResourceParams.PageSize,
                    type = assetResourceParams.Type,
                    search = assetResourceParams.Search
                });
            default:
                return Url.Link("GetAssets", new
                {
                    orderBy = assetResourceParams.OrderBy,
                    page = assetResourceParams.Page,
                    pageSize = assetResourceParams.PageSize,
                    type = assetResourceParams.Type,
                    search = assetResourceParams.Search
                });

        }
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

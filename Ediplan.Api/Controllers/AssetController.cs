using Ediplan.Api.Utility;
using Ediplan.Application.Features.Assets.Commands.DeleteAsset;
using Ediplan.Application.Features.Assets.Queries.GetAssetDetail;
using Ediplan.Application.Features.Assets.Queries.GetAssetsList;
using Ediplan.Application.Helpers;
using Ediplan.Application.Models;
using Ediplan.Application.Services;
using Ediplan.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json;

namespace Ediplan.Api.Controllers;

[ApiController]
[Route("api/assets")]
public class AssetController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPropertyMappingService _propertyMappingService;
    private readonly IPropertyCheckerService _propertyCheckerService;
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public AssetController(IMediator mediator, IPropertyMappingService propertyMappingService, IPropertyCheckerService propertyCheckerService, ProblemDetailsFactory problemDetailsFactory)
    {
        _mediator = mediator ??
            throw new ArgumentNullException(nameof(mediator));
        _propertyMappingService = propertyMappingService ??
            throw new ArgumentNullException(nameof(propertyMappingService));
        _propertyCheckerService = propertyCheckerService ??
            throw new ArgumentNullException(nameof(propertyCheckerService));
        _problemDetailsFactory = problemDetailsFactory ??
            throw new ArgumentNullException(nameof(problemDetailsFactory));
    }

    /// <summary>
    /// Returns all Assets, common properties only
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    [HttpGet(Name = "GetAssets")]
    public async Task<ActionResult<AssetListVm>> GetAssets([FromQuery] GetAssetsListQuery assetResourceParams)
    {
        // Check sortBy field mapping
        if (!_propertyMappingService.ValidMappingExistsFor<AssetListVm, Asset>(
            assetResourceParams.SortBy))
        {
            return BadRequest();
        }

        // Check selected shaping fields exist
        if (!_propertyCheckerService.TypeHasProperties<AssetListVm>(assetResourceParams.Fields))
        {
            return BadRequest(
                _problemDetailsFactory.CreateProblemDetails(
                    HttpContext,
                    statusCode: 400,
                    detail: $"Not all requested data shaping fields exist on " +
                        $"the resource: {assetResourceParams.Fields}"));
        }

        // Get results
        var result = await _mediator.Send(assetResourceParams);
        if (result == null || result.Count == 0)
        {
            return NoContent();
        }

        // Get pagination links
        var previousPageLink = result.HasPrevious ? CreateAssetsResourceUri(
            assetResourceParams, ResourceUriType.PreviousPage) : null;

        var nextPageLink = result.HasNext ? CreateAssetsResourceUri(
            assetResourceParams, ResourceUriType.NextPage) : null;

        // Create pagination metadata and add to response headers
        var paginationMetadata = new
        {
            totalCount = result.TotalCount,
            pageSize = result.PageSize,
            currentPage = result.CurrentPage,
            totalPages = result.TotalPages,
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        // Create links
        var links = CreateLinksForAssets(assetResourceParams,
            result.HasNext,
            result.HasPrevious);

        var shapedData = result.AsEnumerable().ShapeData(assetResourceParams.Fields);

        var shapedDataWithLinks = shapedData.Select(asset =>
        {
            var assetsAsDictionary = asset as IDictionary<string, object?>;
            assetsAsDictionary.Add("links", links);
            return assetsAsDictionary;
        });

        var linkedCollectionResource = new
        {
            value = shapedDataWithLinks,
            links = links
        };

        return Ok(linkedCollectionResource);
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
                    sortBy = assetResourceParams.SortBy,
                    page = assetResourceParams.Page - 1,
                    pageSize = assetResourceParams.PageSize,
                    type = assetResourceParams.Type,
                    search = assetResourceParams.Search
                });
            case ResourceUriType.NextPage:
                return Url.Link("GetAssets", new
                {
                    sortBy = assetResourceParams.SortBy,
                    page = assetResourceParams.Page + 1,
                    pageSize = assetResourceParams.PageSize,
                    type = assetResourceParams.Type,
                    search = assetResourceParams.Search
                });
            default:
                return Url.Link("GetAssets", new
                {
                    sortBy = assetResourceParams.SortBy,
                    page = assetResourceParams.Page,
                    pageSize = assetResourceParams.PageSize,
                    type = assetResourceParams.Type,
                    search = assetResourceParams.Search
                });

        }
    }

    private IEnumerable<LinkDto> CreateLinksForAssets(GetAssetsListQuery assetResourceParams,
        bool hasNext, bool hasPrevious)
    {
        var links = new List<LinkDto>();

        if (string.IsNullOrWhiteSpace(assetResourceParams.Fields))
        {

        }

        // Self
        links.Add(new(CreateAssetsResourceUri(
            assetResourceParams, ResourceUriType.Current), "self", "GET"));

        // next
        if (hasNext)
        {
            links.Add(new(CreateAssetsResourceUri(
                assetResourceParams, ResourceUriType.NextPage), "next", "GET"));
        }

        // previous
        if (hasPrevious)
        {
            links.Add(new(CreateAssetsResourceUri(
                assetResourceParams, ResourceUriType.PreviousPage), "previous", "GET"));
        }

        return links;
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

    private IEnumerable<LinkDto> CreateLinksForAsset(int id, string? fields)
    {
        var links = new List<LinkDto>();

        if (string.IsNullOrWhiteSpace(fields))
        {
            links.Add(
                new (Url.Link("GetAssetById", new { id }),
                "self",
                "GET"));
        }
        else
        {
            links.Add(
                new (Url.Link("GetAssetById", new { id, fields }),
                "self",
                "GET"));
        }

        // Add other links here
        links.Add(
            new(Url.Link("DeleteAsset", new { id }),
            "delete_self",
            "DELETE"));

        return links;
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

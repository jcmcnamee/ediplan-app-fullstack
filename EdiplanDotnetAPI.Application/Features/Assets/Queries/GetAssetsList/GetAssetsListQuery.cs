using EdiplanDotnetAPI.Application.Helpers;
using MediatR;

namespace EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetsList;
public class GetAssetsListQuery : IRequest<PagedList<AssetListVm>>
{
    const int maxPageSize = 20;

    // Filters
    public string? Type { get; set; }

    // Search & OrderBy
    public string? Search {  get; set; }
    public string OrderBy { get; set; } = "CreatedDate";

    public int Page { get; set; } = 1;

    private int _pageSize = 5;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
    }

}

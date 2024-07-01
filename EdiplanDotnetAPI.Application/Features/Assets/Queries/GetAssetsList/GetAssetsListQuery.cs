using EdiplanDotnetAPI.Application.Helpers;
using MediatR;

namespace EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetsList;
public class GetAssetsListQuery : IRequest<PagedList<AssetListVm>>
{
    const int maxPageSize = 20;

    // Filters
    public string? Type { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    // Search & Sort
    public string? Search {  get; set; }
    public string SortBy { get; set; } = "CreatedDate";

    // Shaping
    public string? Fields { get; set; }

    // Pagination
    public int Page { get; set; } = 1;

    private int _pageSize = 5;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
    }

}

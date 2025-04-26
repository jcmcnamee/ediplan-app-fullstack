using Ediplan.Application.Helpers;
using MediatR;

namespace Ediplan.Api.Utility;

public class PaginationMetadataService : IPaginationMetadataService
{
    public object CreatePaginationMetadata<T>(PagedList<T> pagedList, string? nextPageLink, string? prevPageLink)
    {
        return new
        {
            totalCount = pagedList.TotalCount,
            pageSize = pagedList.PageSize,
            currentPage = pagedList.CurrentPage,
            totalPages = pagedList.TotalPages,
            prevPageLink,
            nextPageLink
        };
    }
}

using Ediplan.Application.Helpers;
using MediatR;

namespace Ediplan.Api.Utility;

public interface IPaginationMetadataService
{
    object CreatePaginationMetadata<T>(PagedList<T> pagedList, string? nextPageLink, string? prevPageLink);
}

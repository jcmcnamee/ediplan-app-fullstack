namespace Ediplan.Application.Helpers;

public interface IPagedList<T> : IList<T>
{
    int CurrentPage { get; }
    bool HasNext { get; }
    bool HasPrevious { get; }
    int PageSize { get; }
    int TotalCount { get; }
    int TotalPages { get; }
}
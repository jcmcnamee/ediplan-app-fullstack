using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Helpers;
public class PagedList<T> : List<T>, IPagedList<T>
{
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)PageSize);
        AddRange(items);
    }

    // Do not call constructor directly, instead use CreateAsync so that we can pass through IQueryable returned from LINQ
    //public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    //{
    //    // Call database to retrieve count
    //    var count = source.Count();

    //    // Second call to retrieve paged data
    //    var items = await source.Skip((pageNumber - 1) * pageSize)
    //        .Take(pageSize)
    //        .ToListAsync();

    //    // Call our constructor
    //    return new PagedList<T>(items, count, pageNumber, pageSize);
    //}
}

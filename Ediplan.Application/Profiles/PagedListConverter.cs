using AutoMapper;
using Ediplan.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Profiles;
public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
{
    public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
    {
        return new PagedList<TDestination>(context.Mapper.Map<List<TSource>, List<TDestination>>(source), source.TotalCount, source.CurrentPage, source.PageSize);
    }
}

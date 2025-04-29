using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Ediplan.Api.Utility;

public class UrlHelperProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUrlHelperFactory _urlHelperFactory;

    public UrlHelperProvider(IHttpContextAccessor httpContextAccessor, IUrlHelperFactory urlHelperFactory)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _urlHelperFactory = urlHelperFactory ?? throw new ArgumentNullException(nameof(urlHelperFactory));
    }

    public IUrlHelper GetUrlHelper()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            throw new InvalidOperationException("No active HttpContext.");
        }

        var actionContext = new ActionContext(
            httpContext,
            httpContext.GetRouteData(),
            new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()
        );

        return _urlHelperFactory.GetUrlHelper(actionContext);
    }
}


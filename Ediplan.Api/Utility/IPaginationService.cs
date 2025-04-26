using Ediplan.Api.Utility;
using MediatR;

namespace Ediplan.Application.Services;
internal interface IPaginationService
{
    public string CreateResourceUri(string routeName, IRequest resourceParams, ResourceUriType type);
}

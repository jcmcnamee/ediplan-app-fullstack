using EdiplanDotnetAPI.Application.Contracts;
using EdiplanDotnetAPI.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetDetail;
public class GetAssetDetailQuery : IRequest<IAssetDetailVm>
{
    public int Id { get; set; }
}

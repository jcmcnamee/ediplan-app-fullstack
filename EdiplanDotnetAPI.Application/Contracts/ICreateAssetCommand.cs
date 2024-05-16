using EdiplanDotnetAPI.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Contracts;
public interface ICreateAssetCommand : IRequest<BaseResponse>
{
}


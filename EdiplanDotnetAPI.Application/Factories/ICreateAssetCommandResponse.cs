using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts;
using EdiplanDotnetAPI.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Factories;
public interface ICreateAssetCommandResponse
{
    Task<CreateAssetCommandResponse> CreateResponse(ICreateAssetCommand command, IMapper mapper, CancellationToken cancellationToken);
}

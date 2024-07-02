using AutoMapper;
using Ediplan.Application.Contracts;
using Ediplan.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Factories;
public interface ICreateAssetCommandResponse
{
    Task<CreateAssetCommandResponse> CreateResponse(ICreateAssetCommand command, IMapper mapper, CancellationToken cancellationToken);
}

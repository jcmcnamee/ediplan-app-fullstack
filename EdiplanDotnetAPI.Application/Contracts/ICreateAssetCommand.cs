using EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateAsset;
using EdiplanDotnetAPI.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Contracts;
public interface ICreateAssetCommand : IRequest<CreateAssetCommandResponse>
{
    public string Name { get; set; }
}


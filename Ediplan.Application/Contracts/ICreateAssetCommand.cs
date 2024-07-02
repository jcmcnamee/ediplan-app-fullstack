using Ediplan.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Contracts;
public interface ICreateAssetCommand : IRequest<CreateAssetCommandResponse>
{
    public string Name { get; set; }
}


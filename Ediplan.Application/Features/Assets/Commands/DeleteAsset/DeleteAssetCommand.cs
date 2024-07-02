using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Features.Assets.Commands.DeleteAsset;
public class DeleteAssetCommand : IRequest
{
    public int Id { get; set; }
}

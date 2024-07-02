using Ediplan.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Features.Assets.Commands.CreateRoom;
public class CreateRoomCommand : ICreateAssetCommand
{
    public string Name { get; set; } = string.Empty;
    public decimal? Rate { get; set; }
    public decimal? RateUnit { get; set; }
    public string? UsedFor { get; set; }
    public string? Description { get; set; }
}

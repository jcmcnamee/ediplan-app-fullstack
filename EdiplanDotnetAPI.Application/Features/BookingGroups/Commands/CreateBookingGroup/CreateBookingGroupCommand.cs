using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.BookingGroups.Commands.CreateBookingGroup;
internal class CreateBookingGroupCommand : IRequest<CreateBookingGroupCommandResponse>
{
    public string Name { get; set; } = string.Empty;
}

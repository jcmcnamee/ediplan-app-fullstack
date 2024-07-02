using MediatR;

namespace Ediplan.Application.Features.BookingGroups.Commands.CreateBookingGroup;
public class CreateBookingGroupCommand : IRequest<CreateBookingGroupCommandResponse>
{
    public string Name { get; set; } = "New group";
}

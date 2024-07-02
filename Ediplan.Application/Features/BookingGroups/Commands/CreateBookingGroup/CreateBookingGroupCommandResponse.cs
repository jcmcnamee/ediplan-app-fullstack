using Ediplan.Application.Responses;

namespace Ediplan.Application.Features.BookingGroups.Commands.CreateBookingGroup;
public class CreateBookingGroupCommandResponse : BaseResponse
{
    public CreateBookingGroupCommandResponse(): base()
    {

    }

    public CreateBookingGroupDto BookingGroup { get; set; } = default!;
}
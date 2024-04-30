using EdiplanDotnetAPI.Application.Responses;

namespace EdiplanDotnetAPI.Application.Features.BookingGroups.Commands.CreateBookingGroup;
public class CreateBookingGroupCommandResponse : BaseResponse
{
    public CreateBookingGroupCommandResponse(): base()
    {

    }

    public CreateBookingGroupDto BookingGroup { get; set; } = default!;
}
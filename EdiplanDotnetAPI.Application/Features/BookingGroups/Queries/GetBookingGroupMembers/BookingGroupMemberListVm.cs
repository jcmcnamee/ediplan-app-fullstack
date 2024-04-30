namespace EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupMembers;
public class BookingGroupMemberListVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<BookingGroupMemberDto> Bookings { get; set; }
}

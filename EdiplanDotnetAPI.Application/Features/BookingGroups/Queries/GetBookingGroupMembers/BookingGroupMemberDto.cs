namespace EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupMembers;
public class BookingGroupMemberDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsConfirmed { get; set; }
}

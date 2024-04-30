namespace EdiplanDotnetAPI.Domain.Entities;

public class Production
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "New Production";

    // Navigation properties
    public List<Booking> Bookings { get; } = new();
    public List<Person> People { get; } = new();

}

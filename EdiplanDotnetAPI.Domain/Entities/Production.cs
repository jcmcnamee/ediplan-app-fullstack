namespace EdiplanDotnetAPI.Domain.Entities;

public class Production
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "New Production";
    
    // Navigation properties
    public ICollection<Booking> Bookings{ get; } = new List<Booking>();
    public ICollection<Person> People { get; } = new List<Person>();

}

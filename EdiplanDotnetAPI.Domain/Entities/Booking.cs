using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EdiplanDotnetAPI.Domain.Common;

namespace EdiplanDotnetAPI.Domain.Entities;

public class Booking : AuditableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Name { get; set; } = "New booking.";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsConfirmed { get; set; } = true;
    public string? Notes { get; set; }

    // Foreign keys
    public Guid? ProductionId { get; set; }
    public Guid? LocationId { get; set; }
    
    // Navigation properties
    public List<BookingGroup> BookingGroups { get; set; } = new List<BookingGroup>();
    public List<Asset> Asset { get; set; } = new List<Asset>();
    public Production? Production { get; set; }
    public Location? Location { get; set; }

    public Booking(string name)
    {
        Name = name;
    }

}

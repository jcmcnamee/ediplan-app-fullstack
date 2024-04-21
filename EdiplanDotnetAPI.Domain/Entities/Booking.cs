using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EdiplanDotnetAPI.Domain.Common;

namespace EdiplanDotnetAPI.Domain.Entities;

public class Booking : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsConfirmed { get; set; } = true;
    public string? Notes { get; set; }

    // Foreign keys
    public Guid? ProductionId { get; set; }
    public Guid? LocationId { get; set; }
    
    // Navigation properties
    public ICollection<BookingGroup> BookingGroups { get; set; } = new List<BookingGroup>();
    public ICollection<Asset> Asset { get; set; } = new List<Asset>();
    public Production? Production { get; set; }
    public Location? Location { get; set; }
}

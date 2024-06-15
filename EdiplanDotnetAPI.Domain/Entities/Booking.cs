using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EdiplanDotnetAPI.Domain.Common;

namespace EdiplanDotnetAPI.Domain.Entities;

public class Booking : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }

    // Foreign keys
    public Guid? ProductionId { get; set; }
    public Guid? LocationId { get; set; }

    // Navigation properties
    public List<BookingGroup> BookingGroups { get; set; } = new();
    public List<Asset> Assets { get; set; } = new();
    public Production? Production { get; set; }
    public Location? Location { get; set; }
}

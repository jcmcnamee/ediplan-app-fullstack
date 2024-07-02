using System.ComponentModel.DataAnnotations.Schema;
using Ediplan.Domain.Entities;

namespace Ediplan.Domain.Common;

public abstract class Asset : AuditableEntity
{
    public int Id { get; set; }
    public abstract string Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal? Rate { get; set; }
    public decimal? RateUnit { get; set; }

    // Navigation properties
    public List<AssetGroup> AssetGroups { get; } = new();
    public List<Booking> Bookings { get; } = new();

}
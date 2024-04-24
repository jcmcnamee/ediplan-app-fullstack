using System.ComponentModel.DataAnnotations.Schema;
using EdiplanDotnetAPI.Domain.Entities;

namespace EdiplanDotnetAPI.Domain.Common;

public abstract class Asset : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "New Asset";
    public decimal? Rate { get; set; }
    public decimal? RateUnit { get; set; }
    public decimal? Value { get; set; }

    // Navigation properties
    public ICollection<AssetGroup> AssetGroups { get; set; } = new List<AssetGroup>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

}
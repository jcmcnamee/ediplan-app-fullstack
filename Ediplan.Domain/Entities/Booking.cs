using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ediplan.Domain.Common;

namespace Ediplan.Domain.Entities;

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

    public override string ToString()
    {
        var bookingGroupsString = BookingGroups != null ? string.Join(", ", BookingGroups.Select(bg => bg.Id)) : "None";
        var assetsString = Assets != null ? string.Join(", ", Assets.Select(a => a.Id)) : "None";
        var productionString = Production != null ? Production.Id.ToString() : "None";
        var locationString = Location != null ? Location.Id.ToString() : "None";

        return $"Booking Id: {Id}; Name: {Name}; Start Date: {StartDate}; End Date: {EndDate}; Status: {Status}; Notes: {Notes}; " +
               $"Production ID: {productionString}; Location ID: {locationString}; " +
               $"BookingGroup Ids: {bookingGroupsString}; Asset Ids: {assetsString}";
    }
}

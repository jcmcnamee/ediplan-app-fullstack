using EdiplanDotnetAPI.Domain.Common;

namespace EdiplanDotnetAPI.Domain.Entities;

public class Person : Asset
{
    public string? Role { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool IsStaff { get; set; }
}

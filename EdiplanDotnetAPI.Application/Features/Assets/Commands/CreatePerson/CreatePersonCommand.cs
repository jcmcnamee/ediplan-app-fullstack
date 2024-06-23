using EdiplanDotnetAPI.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Assets.Commands.CreatePerson;
public class CreatePersonCommand : ICreateAssetCommand
{
    public string Name { get; set; } = string.Empty;
    public decimal? Rate { get; set; }
    public decimal? RateUnit { get; set; }
    public string? Role { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool IsStaff { get; set; }
}

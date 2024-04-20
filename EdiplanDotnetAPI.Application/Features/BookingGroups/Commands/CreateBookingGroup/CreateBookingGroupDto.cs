using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.BookingGroups.Commands.CreateBookingGroup;
internal class CreateBookingGroupDto
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
}

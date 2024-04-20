using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupsList;

public class BookingGroupListVm
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}


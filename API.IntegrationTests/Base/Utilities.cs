using EdiplanDotnetAPI.Domain.Entities;
using EdiplanDotnetAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.IntegrationTests.Base;
public class Utilities
{
    public static void InitializeDbForTests(EdiplanDbContext context)
    {
        context.BookingGroups.Add(new BookingGroup
        {
            Id = 1,
            Name = "Offline"
        });
        context.BookingGroups.Add(new BookingGroup
        {
            Id = 2,
            Name = "Online"
        });
        context.BookingGroups.Add(new BookingGroup
        {
            Id = 3,
            Name = "Dub"
        });
        context.BookingGroups.Add(new BookingGroup
        {
            Id = 4,
            Name = "Grade"
        });

        context.SaveChanges();
    }
}

using Ediplan.Application.Contracts;
using Ediplan.Domain.Entities;
using Ediplan.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.IntegrationTests;
public class EdiplanDbContextTests
{
    private readonly EdiplanDbContext _ediplanDbContext;
    private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
    private readonly string _loggedInUserId;

    public EdiplanDbContextTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<EdiplanDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _loggedInUserId = "00000000-0000-0000-0000-000000000000";
        _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
        _loggedInUserServiceMock.Setup(m => m.UserId).Returns(_loggedInUserId);

        _ediplanDbContext = new EdiplanDbContext(dbContextOptions, _loggedInUserServiceMock.Object);
    }

    [Fact]
    public async void Save_SetCreatedByProperty()
    {
        var booking = new Booking()
        {
            StartDate = DateTime.UtcNow.AddMonths(2),
            EndDate = DateTime.UtcNow.AddMonths(2).AddDays(5),
            Status = "provisional",
            Notes = "Pre-production meetings."
        };

        _ediplanDbContext.Bookings.Add(booking);
        await _ediplanDbContext.SaveChangesAsync();

        booking.CreatedBy.ShouldBe(_loggedInUserId);
    }
}

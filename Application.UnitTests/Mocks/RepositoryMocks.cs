using Ediplan.Application.Contracts.Persistence;
using Ediplan.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Mocks;
public class RepositoryMocks
{
    public static Mock<IBookingRepository> GetBookingRepository()
    {
        var bookings = new List<Booking>
        {
            new Booking
            {
                Id = -1,
                StartDate = DateTime.UtcNow.AddDays(2),
                EndDate = DateTime.UtcNow.AddDays(23),
                Status = "provisional",
                Notes = "High-speed internet required for remote editing."
            },
            new Booking
            {
                Id = -2,
                StartDate = DateTime.UtcNow.AddMonths(1),
                EndDate = DateTime.UtcNow.AddMonths(1).AddDays(7),

                Status = "provisional",
                Notes = "Need access to soundproof dubbing studio."
            },
            new Booking
            {
                Id = -3,
                StartDate = DateTime.UtcNow.AddDays(-15),
                EndDate = DateTime.UtcNow.AddDays(-10),
                Status = "confirmed",
                Notes = "Final editing phase."
            },
            new Booking
            {
                Id = -4,
                StartDate = DateTime.UtcNow.AddMonths(-2),
                EndDate = DateTime.UtcNow.AddDays(-5),
                Status = "confirmed",
                Notes = "Location scouting."
            },
            new Booking
            {
                Id = -5,
                StartDate = DateTime.UtcNow.AddMonths(-3),
                EndDate = DateTime.UtcNow.AddMonths(-2).AddDays(-10),
                Status = "confirmed",
                Notes = "Principal photography."
            },
            new Booking
            {
                Id = -6,
                StartDate = DateTime.UtcNow.AddMonths(2),
                EndDate = DateTime.UtcNow.AddMonths(2).AddDays(5),
                Status = "confirmed",
                Notes = "Pre-production meetings."
            },
            new Booking
            {
                Id = -7,
                StartDate = DateTime.UtcNow.AddDays(-8),
                EndDate = DateTime.UtcNow.AddDays(-5),
                Status = "pending",
                Notes = "Color correction phase."
            },
            new Booking
            {
                Id = -8,
                StartDate = DateTime.UtcNow.AddDays(-20),
                EndDate = DateTime.UtcNow.AddDays(-18),
                Status = "completed",
                Notes = "Sound mixing completed."
            },
            new Booking
            {
                Id = -9,
                StartDate = DateTime.UtcNow.AddDays(-12),
                EndDate = DateTime.UtcNow.AddDays(-11),
                Status = "confirmed",
                Notes = "Reshoots scheduled."
            },
            new Booking
            {
                Id = -10,
                StartDate = DateTime.UtcNow.AddDays(-22),
                EndDate = DateTime.UtcNow.AddDays(-20),
                Status = "cancelled",
                Notes = "Project on hold."
            }
        };

        var mockBookingRepository = new Mock<IBookingRepository>();

        mockBookingRepository.Setup(repo => repo.ListAllAsync())
            .ReturnsAsync(bookings);

        mockBookingRepository.Setup(repo => repo.AddAsync(It.IsAny<Booking>()))
            .ReturnsAsync(
            (Booking booking) =>
            {
                bookings.Add(booking);
                return booking;
            });

        return mockBookingRepository;
    }

    public static Mock<IAsyncRepository<BookingGroup>> GetGroupRepository()
    {
        //var offlineGuid = Guid.Parse("{bd8249bd-7a0a-4379-a498-3d54d52a7434}");
        //var onlineGuid = Guid.Parse("{af4fa909-0323-4ed1-8bbb-56c22882b753}");
        //var dubGuid = Guid.Parse("{64f941b3-6eca-443d-b4a4-50f364b26eac}");
        //var gradeGuid = Guid.Parse("{cb17689f-f733-478c-8fef-42cbc21c2a65}");

        var groups = new List<BookingGroup>
        {
            new BookingGroup
            {
                Id = 1,
                Name = "Offline"
            },
            new BookingGroup
            {
                Id = 2,
                Name = "Online"
            },
            new BookingGroup
            {
                Id = 3,
                Name = "Dub"
            },
            new BookingGroup
            {
                Id = 4,
                Name = "Grade"
            }
        };

        // Create mock implementation of IAsyncRepository
        var mockGroupRepository = new Mock<IAsyncRepository<BookingGroup>>();

        // State that when you call ListAllAsync it should just return all groups
        mockGroupRepository.Setup(repo => repo.ListAllAsync())
            .ReturnsAsync(groups);

        // State that when you AddAsync you should add it to the list
        mockGroupRepository.Setup(repo => repo.AddAsync(It.IsAny<BookingGroup>()))
            .ReturnsAsync(
            (BookingGroup group) =>
            {
                groups.Add(group);
                return group;
            });

        return mockGroupRepository;

    }
}

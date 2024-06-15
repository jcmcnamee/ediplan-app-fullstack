using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Mocks;
public class RepositoryMocks
{
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

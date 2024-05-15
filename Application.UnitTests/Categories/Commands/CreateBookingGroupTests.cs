using Application.UnitTests.Mocks;
using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Features.BookingGroups.Commands.CreateBookingGroup;
using EdiplanDotnetAPI.Application.Profiles;
using EdiplanDotnetAPI.Domain.Entities;
using Moq;
using Shouldly;

namespace Application.UnitTests.Categories.Commands;
public class CreateBookingGroupTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IAsyncRepository<BookingGroup>> _mockGroupRepository;
    public CreateBookingGroupTests()
    {
        _mockGroupRepository = RepositoryMocks.GetGroupRepository();
        var configProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configProvider.CreateMapper();
    }

    [Fact]
    public async Task Handle_ValidGroup_AddedToGroupRepo()
    {
        var handler = new CreateBookingGroupCommandHandler(_mapper, _mockGroupRepository.Object);

        await handler.Handle(new CreateBookingGroupCommand()
        {
            Name = "Test"
        }, CancellationToken.None);

        var allGroups = await _mockGroupRepository.Object.ListAllAsync();
        allGroups.Count.ShouldBe(5);
    }
}

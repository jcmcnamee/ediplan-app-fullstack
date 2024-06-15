using Application.UnitTests.Mocks;
using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Exceptions;
using EdiplanDotnetAPI.Application.Features.BookingGroups.Commands.CreateBookingGroup;
using EdiplanDotnetAPI.Application.Profiles;
using EdiplanDotnetAPI.Domain.Entities;
using Moq;

namespace Application.UnitTests.BookingGroups.Commands;
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
        var sut = new CreateBookingGroupCommandHandler(_mapper, _mockGroupRepository.Object);

        await sut.Handle(new CreateBookingGroupCommand()
        {
            Name = "Test"
        }, CancellationToken.None);
        var result = await _mockGroupRepository.Object.ListAllAsync();

        // Shouldly: allGroups.Count.ShouldBe(5);
        Assert.Equal(5, result.Count);
    }

    [Theory]
    [InlineData("This is a very very long string that is over 50 characters long")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("     ")]
    //[InlineData("NameWithSpecialCharacters!@#")]
    public async Task Handle_InvalidGroup_ValidationErrorReturned(string input) 
    {
        var sut = new CreateBookingGroupCommandHandler(_mapper, _mockGroupRepository.Object);

        var result = await sut.Handle(new CreateBookingGroupCommand()
        {
            Name = input
        }, CancellationToken.None);

        Assert.NotNull(result.ValidationErrors);
        Assert.Contains(result.ValidationErrors, e => e.Contains("Name"));
    }


}

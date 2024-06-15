using Application.UnitTests.Mocks;
using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupsList;
using EdiplanDotnetAPI.Application.Profiles;
using EdiplanDotnetAPI.Domain.Entities;
using Moq;
using Xunit;


namespace Application.UnitTests.BookingGroups.Queries;
public class GetBookingGroupQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IAsyncRepository<BookingGroup>> _mockGroupRepository;
    public GetBookingGroupQueryHandlerTests()
    {
        // Test double (mock)
        _mockGroupRepository = RepositoryMocks.GetGroupRepository();
        var configProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configProvider.CreateMapper();
    }

    [Fact]
    public async Task GetGroupsListTest()
    {
        var handler = new GetBookingGroupListQueryHandler(_mapper, _mockGroupRepository.Object);

        var result = await handler.Handle(new GetBookingGroupListQuery(), CancellationToken.None);

        //result.ShouldBeOfType<List<BookingGroupListVm>>();

        //result.Count.ShouldBe(4);

        Assert.IsType<List<BookingGroupListVm>>(result);
    }
} 

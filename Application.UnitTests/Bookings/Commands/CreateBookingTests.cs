using Application.UnitTests.Mocks;
using AutoMapper;
using Ediplan.Application.Contracts.Infrastructure;
using Ediplan.Application.Contracts.Persistence;
using Ediplan.Application.Exceptions;
using Ediplan.Application.Features.Bookings.Commands.CreateBooking;
using Ediplan.Application.Profiles;
using Ediplan.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Bookings.Commands;
public class CreateBookingTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IBookingRepository> _mockBookingRepository;
    private readonly Mock<IAssetRepository> _mockAssetRepository;
    private readonly Mock<IBookingGroupRepository> _mockBookingGroupRepository;
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly Mock<ILogger<CreateBookingCommandHandler>> _mockLogger;

    public CreateBookingTests()
    {
        _mockBookingRepository = RepositoryMocks.GetBookingRepository();
        var configProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configProvider.CreateMapper();
        _mockAssetRepository = new Mock<IAssetRepository>();
        _mockBookingGroupRepository = new Mock<IBookingGroupRepository>();
        _mockEmailService = new Mock<IEmailService>();
        _mockLogger = new Mock<ILogger<CreateBookingCommandHandler>>();
    }

    [Fact]
    public async Task Handle_ValidBooking_AddedToBookingRepo()
    {
        var initialCount = (await _mockBookingRepository.Object.ListAllAsync()).Count;

        var sut = new CreateBookingCommandHandler(
            _mapper,
            _mockBookingRepository.Object,
            _mockAssetRepository.Object,
            _mockBookingGroupRepository.Object,
            _mockEmailService.Object,
            _mockLogger.Object);

        await sut.Handle(new CreateBookingCommand()
        {
            Name = "Valid test booking",
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(2)
        }, CancellationToken.None);

        var result = await _mockBookingRepository.Object.ListAllAsync();

        Assert.Equal(initialCount + 1, result.Count);
    }

    [Fact]
    public async Task Handle_InvalidDatesBooking_ExceptionThrownBookingNotAdded()
    {
        var initialCount = (await _mockBookingRepository.Object.ListAllAsync()).Count;

        var sut = new CreateBookingCommandHandler(
            _mapper,
            _mockBookingRepository.Object,
            _mockAssetRepository.Object,
            _mockBookingGroupRepository.Object,
            _mockEmailService.Object,
            _mockLogger.Object
            );

        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await sut.Handle(new CreateBookingCommand()
            {
                Name = "Invalid test booking",
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(1)
            }, CancellationToken.None);
        });

        var result = await _mockBookingRepository.Object.ListAllAsync();

        Assert.Equal(initialCount, result.Count);
    }

    [Theory]
    [InlineData("This is a very very long string that is over 50 characters long")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("     ")]
    public async Task Handle_InvalidNamesBooking_ExceptionThrownBookingNotAdded(string input)
    {
        var initialCount = (await _mockBookingRepository.Object.ListAllAsync()).Count;

        var sut = new CreateBookingCommandHandler(
            _mapper,
            _mockBookingRepository.Object,
            _mockAssetRepository.Object,
            _mockBookingGroupRepository.Object,
            _mockEmailService.Object,
            _mockLogger.Object
            );

        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await sut.Handle(new CreateBookingCommand()
            {
                Name = input,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2)
            }, CancellationToken.None);
        });

        var result = await _mockBookingRepository.Object.ListAllAsync();

        Assert.Equal(initialCount, result.Count);
    }
}

using API.IntegrationTests.Base;
using Ediplan.Application.Features.BookingGroups.Queries.GetBookingGroupsList;
using System.Text.Json;

namespace API.IntegrationTests.Controllers;
public class BookingGroupControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public BookingGroupControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ReturnsSuccessResult()
    {
        var client = _factory.GetAnonymousClient();

        var response = await client.GetAsync("/api/bookings/groups/all");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<List<BookingGroupListVm>>(responseString);

        Assert.IsType<List<BookingGroupListVm>>(result);
        Assert.NotEmpty(result);
    }
}

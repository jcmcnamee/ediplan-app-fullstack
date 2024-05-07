using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Infrastructure;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Domain.Entities;
using MediatR;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsExport;
public class GetBookingsExportQueryHandler : IRequestHandler<GetBookingsExportQuery, BookingExportFileVm>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Booking> _bookingRepository;
    private readonly ICsvExporter _csvExporter;

    public GetBookingsExportQueryHandler(IMapper mapper, IAsyncRepository<Booking> bookingRepository, ICsvExporter csvExporter)
    {
        _mapper = mapper;
        _bookingRepository = bookingRepository;
        _csvExporter = csvExporter;
    }
    public async Task<BookingExportFileVm> Handle(GetBookingsExportQuery request, CancellationToken cancellationToken)
    {
        var allBookings = _mapper.Map<List<BookingExportDto>>((await _bookingRepository.ListAllAsync()).OrderBy(x => x.StartDate));

        var fileData = _csvExporter.ExportBookingsToCsv(allBookings);

        var bookingExportFileDto = new BookingExportFileVm()
        {
            ContentType = "text/csv",
            Data = fileData,
            BookingExportFileName = $"{Guid.NewGuid()}.csv"
        };

        return bookingExportFileDto;
    }
}

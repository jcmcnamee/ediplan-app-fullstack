using CsvHelper;
using Ediplan.Application.Contracts.Infrastructure;
using Ediplan.Application.Features.Bookings.Queries.GetBookingsExport;
using System.Globalization;


namespace Ediplan.Infrastructure.FileExport;
public class CsvExporter : ICsvExporter
{
    public byte[] ExportBookingsToCsv(List<BookingExportDto> bookingsExportDtos)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(bookingsExportDtos);
        }
        return memoryStream.ToArray();
    }
}

using Ediplan.Application.Features.Bookings.Queries.GetBookingsExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Contracts.Infrastructure;
public interface ICsvExporter
{
    byte[] ExportBookingsToCsv(List<BookingExportDto> bookingExportDtos);
}

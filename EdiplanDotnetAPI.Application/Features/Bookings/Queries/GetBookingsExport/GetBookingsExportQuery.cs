using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsExport;
public class GetBookingsExportQuery : IRequest<BookingExportFileVm>
{
}

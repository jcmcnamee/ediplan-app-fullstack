using Ediplan.Api.Utility;
using Ediplan.Application.Features.BookingGroups.Queries.GetBookingGroupsList;
using Ediplan.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Services;
internal interface IBookingUriService
{
    string? CreateBookingsResourceUri(GetBookingGroupListQuery bookingResourceParams, ResourceUriType type);
}

using Ediplan.Application.Features.Bookings.Commands.UpdateBooking;
using Ediplan.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Features.Bookings.Commands.PatchBooking;
public class PatchBookingCommand : IRequest
{
    public int Id { get; set; }
    public JsonPatchDocument<UpdateBookingDto> Booking { get; set; } = default!;
}

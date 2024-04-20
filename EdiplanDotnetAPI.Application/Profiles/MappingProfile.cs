using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupDetail;
using EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupsList;
using EdiplanDotnetAPI.Application.Features.Bookings.Commands.CreateBooking;
using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingDetail;
using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;
using EdiplanDotnetAPI.Domain.Entities;

namespace EdiplanDotnetAPI.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Booking Query objects
        CreateMap<Booking, BookingListVm>().ReverseMap();
        CreateMap<Booking, BookingDetailVm>().ReverseMap();
        CreateMap<Production, ProductionDto>().ReverseMap();

        // Booking command objects
        CreateMap<Booking, CreateBookingCommand>().ReverseMap();

        // Booking Group query objects
        CreateMap<BookingGroup, BookingGroupListVm>();
        CreateMap<BookingGroup, BookingGroupMemberListVm>();
    }
}

using AutoMapper;
using EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetsList;
using EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupMembers;
using EdiplanDotnetAPI.Application.Features.BookingGroups.Queries.GetBookingGroupsList;
using EdiplanDotnetAPI.Application.Features.Bookings.Commands.CreateBooking;
using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingDetail;
using EdiplanDotnetAPI.Application.Features.Bookings.Queries.GetBookingsList;
using EdiplanDotnetAPI.Domain.Common;
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

        // Asset Query objects
        CreateMap<Asset, AssetListVm>().ReverseMap();

        // Booking command objects
        CreateMap<Booking, CreateBookingCommand>().ReverseMap();

        // Booking Group query objects
        CreateMap<BookingGroup, BookingGroupListVm>();
        CreateMap<BookingGroup, BookingGroupMemberListVm>();
    }
}

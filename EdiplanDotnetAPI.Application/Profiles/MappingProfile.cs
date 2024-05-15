using AutoMapper;
using EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetsList;
using EdiplanDotnetAPI.Application.Features.BookingGroups.Commands.CreateBookingGroup;
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
        // Create command entity mapping
        CreateMap<Booking, CreateBookingCommand>().ReverseMap();
        CreateMap<BookingGroup, CreateBookingGroupCommand>().ReverseMap();

        // Entity view model mapping
        CreateMap<Booking, BookingListVm>().ReverseMap();
        CreateMap<Booking, BookingDetailVm>().ReverseMap();
        CreateMap<BookingGroup, BookingGroupListVm>();
        CreateMap<BookingGroup, BookingGroupMemberListVm>();
        CreateMap<Asset, AssetListVm>().ReverseMap();

        // Internal DTOs
        CreateMap<Production, ProductionDto>().ReverseMap();

        // Response DTOs
        CreateMap<Booking, CreateBookingDto>().ReverseMap();
        CreateMap<BookingGroup, CreateBookingGroupDto>().ReverseMap();

    }
}

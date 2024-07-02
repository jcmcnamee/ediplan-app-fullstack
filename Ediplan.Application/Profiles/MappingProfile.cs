using AutoMapper;
using Ediplan.Application.Features.Assets.Commands.CreateEquipment;
using Ediplan.Application.Features.Assets.Commands.CreatePerson;
using Ediplan.Application.Features.Assets.Commands.CreateRoom;
using Ediplan.Application.Features.Assets.Queries.GetAssetDetail;
using Ediplan.Application.Features.Assets.Queries.GetAssetsList;
using Ediplan.Application.Features.BookingGroups.Commands.CreateBookingGroup;
using Ediplan.Application.Features.BookingGroups.Queries.GetBookingGroupMembers;
using Ediplan.Application.Features.BookingGroups.Queries.GetBookingGroupsList;
using Ediplan.Application.Features.Bookings.Commands.CreateBooking;
using Ediplan.Application.Features.Bookings.Commands.UpdateBooking;
using Ediplan.Application.Features.Bookings.Queries.GetBookingDetail;
using Ediplan.Application.Features.Bookings.Queries.GetBookingsList;
using Ediplan.Application.Features.Equipment.Queries.GetEquipmentList;
using Ediplan.Application.Helpers;
using Ediplan.Domain.Common;
using Ediplan.Domain.Entities;
using System.Collections.Generic;

namespace Ediplan.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap(typeof(PagedList<>), typeof(PagedList<>)).ConvertUsing(typeof(PagedListConverter<,>));
        //CreateMap(typeof(PagedList<>), typeof(PagedList<>));

        //////////// Bookings //////////////
        // Create
        CreateMap<Booking, CreateBookingCommand>().ReverseMap();
        CreateMap<Booking, CreateBookingDto>().ReverseMap();
        //CreateMap<Booking, CreateBookingDto>()
        //    .ForMember(dest => dest.Assets, opt => opt.MapFrom(
        //        src => src.Assets.Select(asset => new
        //        {
        //            Id = asset.Id,
        //            Name = asset.Name,
        //            Type = asset.Type,
        //            Rate = asset.Rate,
        //            RateUnit = asset.RateUnit,
        //            CreatedDate = asset.CreatedDate,
        //        })));
        // Update
        CreateMap<Booking, UpdateBookingCommand>().ReverseMap();
        // Partial update
        CreateMap<Booking, UpdateBookingDto>().ReverseMap(); // PATCH PAYLOAD, CHECK IF NECESSARY TO USE DTO INSTEAD OF COMMAND
        // Get List
        CreateMap<Booking, BookingListVm>()
            .ForMember(dest => dest.ProductionName, opt => opt.MapFrom(src => src.Production.Name));
        // Get Detail
        CreateMap<Booking, BookingDetailVm>().ReverseMap();

        /////////// Booking Groups /////////////
        ///// Create
        CreateMap<BookingGroup, CreateBookingGroupCommand>().ReverseMap();
        CreateMap<BookingGroup, CreateBookingGroupDto>().ReverseMap();
        CreateMap<BookingGroup, BookingGroupListVm>();
        CreateMap<BookingGroup, BookingGroupMemberListVm>();

        CreateMap<Asset, AssetListVm>().ReverseMap();
        CreateMap<Asset, AssetDto>().ReverseMap();

        CreateMap<Equipment, CreateEquipmentCommand>().ReverseMap();
        CreateMap<Equipment, CreateEquipmentDto>().ReverseMap();
        CreateMap<Equipment, EquipmentListVm>().ReverseMap();
        CreateMap<Equipment, EquipmentDetailVm>().ReverseMap();

        CreateMap<Person, CreatePersonCommand>().ReverseMap();
        CreateMap<Person, PersonDetailVm>().ReverseMap();
        CreateMap<Person, CreatePersonDto>().ReverseMap();


        CreateMap<Room, CreateRoomCommand>().ReverseMap();
        CreateMap<Room, RoomDetailVm>().ReverseMap();
        CreateMap<Room, CreateRoomDto>().ReverseMap();

     
        CreateMap<Production, ProductionDto>().ReverseMap();

    }
}

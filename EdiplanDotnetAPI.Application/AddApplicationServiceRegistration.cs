using AutoMapper.Configuration;
using EdiplanDotnetAPI.Application.Contracts;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Factories;
using EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateAsset;
using EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateEquipment;
using EdiplanDotnetAPI.Application.Features.Assets.Commands.CreatePerson;
using EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateRoom;
using EdiplanDotnetAPI.Application.Responses;
using EdiplanDotnetAPI.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application
{
    public static class AddApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddTransient<IRequestHandler<CreateEquipmentCommand, CreateAssetCommandResponse>, CreateAssetCommandHandler>();
            services.AddTransient<IRequestHandler<CreatePersonCommand, CreateAssetCommandResponse>, CreateAssetCommandHandler>();
            services.AddTransient<IRequestHandler<CreateRoomCommand, CreateAssetCommandResponse>, CreateAssetCommandHandler>();

            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
            services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();



            return services;
        }
    }
}

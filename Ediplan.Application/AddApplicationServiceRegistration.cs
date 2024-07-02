using AutoMapper.Configuration;
using Ediplan.Application.Contracts;
using Ediplan.Application.Contracts.Persistence;
using Ediplan.Application.Factories;
using Ediplan.Application.Features.Assets.Commands.CreateAsset;
using Ediplan.Application.Features.Assets.Commands.CreateEquipment;
using Ediplan.Application.Features.Assets.Commands.CreatePerson;
using Ediplan.Application.Features.Assets.Commands.CreateRoom;
using Ediplan.Application.Responses;
using Ediplan.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application
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

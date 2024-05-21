using EdiplanDotnetAPI.Application.Contracts;
using EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateAsset;
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

            return services;
        }
    }
}

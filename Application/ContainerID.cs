using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using Domain.Interface;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ContainerID
    {



        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IPropertyImageService, PropertyImageService>();
            services.AddScoped<IPropertyTraceService, PropertyTraceService>();

            return services;
        }
    }
}

using Domain.Interface;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ContainerIDInfraestructure
    {


        public static IServiceCollection ConfigureRepository(this IServiceCollection services)
        {

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IPropertyImagesRepository, PropertyImagesRepository>();
            services.AddScoped<IPropertyTraceRepository, PropertyTraceRepository>();

            return services;
        }
    }
}

using Domain.Interface;
using Infraestructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
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

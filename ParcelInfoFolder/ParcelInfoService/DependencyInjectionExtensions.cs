using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParcelInfoService.AppSettings;
using ParcelInfoService.Data.MongoRepository;
using ParcelInfoService.Managers;

namespace ParcelInfoService
{

    public static class DependencyInjectionExtensions
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddParcelServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));
            services.AddScoped<IParcelManager, ParcelManager>();
            services.AddScoped<IParcelInfoRepository, ParcelInfoRepository>();
            return services;
        }
    }
}

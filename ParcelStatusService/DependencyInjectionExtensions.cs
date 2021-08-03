using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParcelStatusService.AppSettings;
using ParcelStatusService.Data.RedisRepository;
using ParcelStatusService.Data.SqlRepository;
using ParcelStatusService.Helper;
using ParcelStatusService.Manager;

namespace ParcelStatusService
{
    public static class DependencyInjectionExtensions
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddParcelServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString(nameof(ConnectionStrings.RedisDatabase));
            });



            services.AddScoped<IParcelStatusManager, ParcelStatusManager>();



            services.AddScoped<IParcelStatusRepositoryRedis, ParselStatusRepositoryRedis>();
            services.AddScoped<IParcelStatusRepositorySql, ParcelStatusRepositorySQL>();

            services.AddScoped<IStatusHelper, StatusHelper>();



            return services;
        }
    }
}

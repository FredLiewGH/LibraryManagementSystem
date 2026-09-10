using LMS.EFCore.Abstractions;
using LMS.EFCore.Data;
using LMS.EFCore.Infrastructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.EFCore.Extensions
{
    public static class EFCoreServiceCollections
    {
        public static IServiceCollection AddEFCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);

            var connectionString = configuration.GetConnectionString("LMSDB");

            services.AddDbContext<LMSDBContext>((serviceProvider, builder) =>
            {
                builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddScoped(typeof(ICRUD<>), typeof(CRUD<>));
            services.AddScoped<IDBCommit, DBCommit>();

            return services;
        }
    }
}

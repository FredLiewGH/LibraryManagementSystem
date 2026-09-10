using LMS.Services.Abstractions;
using LMS.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {            
            services.AddScoped<IAPIResponseBuilder, APIResponseBuilder>();
            services.AddScoped<IBookServices, BookServices>();
            services.AddScoped<IMemberServices, MemberServices>();
            services.AddScoped<IBorrowServices, BorrowServices>();
            return services;
        }
    }
}

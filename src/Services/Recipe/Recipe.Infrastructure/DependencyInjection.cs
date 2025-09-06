using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recipe.Application.Data;
using Recipe.Infrastructure.Data;
using Recipe.Infrastructure.Data.Interceptors;

namespace Recipe.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDBContext>((sp, options) =>
        {
            var interceptors = sp.GetServices<ISaveChangesInterceptor>();
            options.AddInterceptors(interceptors);
            options.UseNpgsql(connString);
        });

        services.AddScoped<IApplicationDbContext, ApplicationDBContext>();

        return services;   
    }
}

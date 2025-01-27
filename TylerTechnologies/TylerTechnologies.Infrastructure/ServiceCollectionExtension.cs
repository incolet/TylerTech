using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TylerTechnologies.Core.Abstracts.Repositories;
using TylerTechnologies.Core.Abstracts.Services;
using TylerTechnologies.Core.Services;
using TylerTechnologies.Infrastructure.Data;
using TylerTechnologies.Infrastructure.Data.Repositories;

namespace TylerTechnologies.Infrastructure;

internal static class ServiceCollectionExtension
{
    internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEfCore(configuration);
        services.AddServices();

        return services;
    }
    private static IServiceCollection AddEfCore(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
        
        services.AddDbContext<EmployeeDbContext>(options =>
            options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(EmployeeDbContext).Assembly.FullName)));
 
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }
}
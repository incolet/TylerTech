using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TylerTechnologies.Api.Endpoints;
using TylerTechnologies.Api.Validators;
using TylerTechnologies.Core.DTOs;
using TylerTechnologies.Infrastructure.Data;

namespace TylerTechnologies.Api.Extensions;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// and <see cref="IApplicationBuilder"/>
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// Scans the assembly for all types that implement <see cref="IEndpoint"/>
    /// and registers them as transient services.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    /// <returns>
    /// returns the <see cref="IServiceCollection"/>
    /// </returns>
    public static IServiceCollection AddEndpoints(
        this IServiceCollection services, 
        Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();
        services.TryAddEnumerable(serviceDescriptors);
        return services;
    }

    /// <summary>
    /// Gets all the endpoints from the service provider and maps them to the application.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="routeGroupBuilder"></param>
    /// <returns>
    /// returns the <see cref="IApplicationBuilder"/>
    /// </returns>
    public static IApplicationBuilder MapEndpoints(
        this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoint> endpoints = app.Services
            .GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }
    
    /// <summary>
    /// Adds the validators to the service collection
    /// </summary>
    /// <param name="services"></param>
    /// <returns>
    /// returns the <see cref="IServiceCollection"/>
    /// </returns>
    public static IServiceCollection AddValidators(
        this IServiceCollection services)
    {
        services.AddScoped<IValidator<AddEmployeeDto>, AddEmployeeDtoValidator>();
        return services;
    }
    
    /// <summary>
    /// This method seeds the database with initial data
    /// </summary>
    /// <param name="app"></param>
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
        dbContext.Database.Migrate();
        
        DataSeeder.EnsureRolesSeed(dbContext);
        DataSeeder.EnsureEmployeeSeed(dbContext);
    }
}
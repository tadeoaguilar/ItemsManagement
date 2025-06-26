using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;
using eItems.Catalog.Application.Behaviors;
using MediatR;

namespace eItems.Catalog.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCatalogApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        // Register MediatR handlers
        services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
        
        // Register FluentValidation validators
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
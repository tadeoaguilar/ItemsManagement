using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace eItems.Shared.Extensions;
public static class MediatRExtentions
{
    public static IServiceCollection AddMediatRWithAssemblies
        (this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assemblies);
      
        });

        services.AddValidatorsFromAssemblies(assemblies);

        return services;
    }
}
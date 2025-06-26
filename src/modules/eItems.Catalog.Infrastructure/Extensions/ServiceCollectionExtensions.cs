using Microsoft.Extensions.DependencyInjection;
using eItems.Catalog.Domain.Repositories;
using eItems.Catalog.Infrastructure.Repositories;

namespace eItems.Catalog.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services)
    {
        // Register repositories
        services.AddScoped<IAssetRepository, AssetRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
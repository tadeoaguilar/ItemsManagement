using eItems.Catalog.Data;
using eItems.Shared.Data.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using eItems.Shared.Data;
using eItems.Catalog.Data.Seed;
namespace eItems.Catalog;

public static class CatalogModule
{
public static IServiceCollection AddCatalogModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add services to the container.

        // Api Endpoint services

        // Application Use Case services
        // Note: These will be registered in the API service project

        // Data - Infrastructure services
        // Note: These will be registered in the API service project

       /* services.AddDbContext<CatalogContext>((sp, options) =>
        {
            
            options.UseNpgsql("eItems");
        });*/

        
        services.AddScoped<IDataSeeder, CatalogDataSeeder>();
      
        return services;
    }

    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        // Configure the HTTP request pipeline.

        // 1. Use Api Endpoint services

        // 2. Use Application Use Case services

        // 3. Use Data - Infrastructure services  
       // app.UseMigration<CatalogContext>();
    
     app.UseMigration<CatalogContext>();
   
        return app;
    }
}

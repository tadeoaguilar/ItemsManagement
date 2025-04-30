using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using eItems.Shared.Data;
using eItems.Identity.Data;
namespace eItems.Identity;
public static class IdentityModule
{
public static IServiceCollection AddIdentityModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add services to the container.

        // Api Endpoint services

        // Application Use Case services       

        // Data - Infrastructure services

       /* services.AddDbContext<CatalogContext>((sp, options) =>
        {
            
            options.UseNpgsql("eItems");
        });*/

        
        //services.AddScoped<IDataSeeder, CatalogDataSeeder>();
      
        return services;
    }

    public static IApplicationBuilder UseIdentityModule(this IApplicationBuilder app)
    {
        // Configure the HTTP request pipeline.

        // 1. Use Api Endpoint services

        // 2. Use Application Use Case services

        // 3. Use Data - Infrastructure services  
       // app.UseMigration<CatalogContext>();
    
     app.UseMigration<IdentityAppDbContext>();
   
        return app;
    }
}

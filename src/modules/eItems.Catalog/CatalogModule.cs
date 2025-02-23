
namespace eItems.Catalog;


public static class CatalogModule
{

 public static IServiceCollection AddCatalogModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CatalogContext>((sp, options) =>
        {           
            options.UseNpgsql("eItems");
        });

        return services;
    }
}


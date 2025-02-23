
using System.Data.Common;
using System.Reflection;

namespace eItems.Catalog.Data
{
    public class CatalogContext : DbContext//, ICatalogDBContext

    {
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options) { }

        public DbSet<Tenant> Tenant => Set<Tenant>();
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {

        optionsBuilder.UseNpgsql("eItems");

    }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.HasDefaultSchema("catalog");
            

            base.OnModelCreating(builder);

        }

        //TODO- Include Database Extension to create Database at startup
    }
}

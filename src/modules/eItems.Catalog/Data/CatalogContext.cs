

namespace eItems.Catalog.Data
{
    public class CatalogContext : DbContext//, ICatalogDBContext

    {
        public CatalogContext() { }
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options) { }

        public DbSet<Tenant> Tenant => Set<Tenant>();
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {

        
        optionsBuilder.UseNpgsql( "Host=localhost:51552;Database=eItems;Username=postgres;Password=C~u3zS1rB429CK(EkrvwbW");
        //optionsBuilder.UseNpgsql("eItems");
    }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.HasDefaultSchema("catalog");
            

            base.OnModelCreating(builder);

        }

        

        //TODO- Include Database Extension to create Database at startup
    }
}

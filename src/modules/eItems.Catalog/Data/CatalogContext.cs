

namespace eItems.Catalog.Data
{
    public class CatalogContext : DbContext//, ICatalogDBContext

    {
        public CatalogContext() { }
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options) { }

        public DbSet<Tenant> Tenant => Set<Tenant>();                
        public DbSet<Company> Company => Set<Company>();
        public DbSet<Organization> Organization => Set<Organization>();
        public DbSet<Location> Location => Set<Location>();
        public DbSet<Asset> Asset => Set<Asset>();
        public DbSet<License> License => Set<License>();
        public DbSet<Manufacturer> Manufacturer => Set<Manufacturer>();
  /*  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {

        
        optionsBuilder.UseNpgsql( "Host=localhost:59001;Database=eItems;Username=postgres;Password=f7~SefwuNfdp*+DtEP_5d7");
        //optionsBuilder.UseNpgsql("eItems");
    }*/

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.HasDefaultSchema("catalog");
            

            base.OnModelCreating(builder);

        }

        

        //TODO- Include Database Extension to create Database at startup
    }
}

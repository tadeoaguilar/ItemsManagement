using System;
using eItems.Identity.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Design;

namespace eItems.Identity.Data;

public class IdentityAppDbContext : IdentityDbContext<ApplicationUser>
{
    public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options)
        : base(options)
    {}


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           
            

            base.OnModelCreating(builder);
            builder.HasDefaultSchema("identity");
        }
    
}
/*
public class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityAppDbContext>
{
    public IdentityAppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityAppDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost:5432;Database=eItems;Username=postgres;Password=C~u3zS1rB429CK(EkrvwbW");

        return new IdentityAppDbContext(optionsBuilder.Options);
    }
}*/
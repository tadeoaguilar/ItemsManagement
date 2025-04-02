using System;
using System.Text.Json;
using Microsoft.Extensions.Hosting;

namespace eItems.Catalog.Extensions;

public static class Extensions
{
 public static void AddApplicationServices(this IHostApplicationBuilder builder)
 {
                                                        
            builder.AddNpgsqlDbContext<CatalogContext>(connectionName: "CatalogDB", configureDbContextOptions: dbContextOptionsBuilder => 
            {
                
                dbContextOptionsBuilder.UseSeeding((context, _) =>
                {
                   
                    var licenseJsonPath = Path.Combine(AppContext.BaseDirectory, "Setup", "licenses.json");
                    var licenseData = File.ReadAllText(licenseJsonPath);
                    var licenses = JsonSerializer.Deserialize<List<License>>(licenseData);
                    var tenantJsonPath = Path.Combine(AppContext.BaseDirectory, "Setup", "tenants.json");
                    var tenantData = File.ReadAllText(tenantJsonPath);
                    var tenants = JsonSerializer.Deserialize<List<Tenant>>(tenantData);

                    var countryJsonPath = Path.Combine(AppContext.BaseDirectory, "Setup", "countries.json");
                    var countryData = File.ReadAllText(countryJsonPath);
                    var countries = JsonSerializer.Deserialize<List<Country>>(countryData);

                    var companyJsonPath = Path.Combine(AppContext.BaseDirectory, "Setup", "companies.json");
                    var companyData = File.ReadAllText (companyJsonPath);
                    var companies = JsonSerializer.Deserialize<List<Company>> (companyData);

                    var organizationJsonPath = Path.Combine(AppContext.BaseDirectory, "Setup", "organizations.json");
                    var organizationData = File.ReadAllText (organizationJsonPath);
                    var organizations = JsonSerializer.Deserialize<List<Organization>> (organizationData);

                    var locationJsonPath = Path.Combine(AppContext.BaseDirectory, "Setup", "locations.json");
                    var locationData = File.ReadAllText (locationJsonPath);
                    var locations = JsonSerializer.Deserialize<List<Location>> (locationData);

                    var manufacturerJsonPath = Path.Combine(AppContext.BaseDirectory, "Setup", "manufacturers.json");
                    var manufacturerData = File.ReadAllText (manufacturerJsonPath);
                    var manufacturers = JsonSerializer.Deserialize<List<Manufacturer>> (manufacturerData);

                    var costcenterJsonPath = Path.Combine(AppContext.BaseDirectory, "Setup", "costcenters.json");
                    var costcenterData = File.ReadAllText (costcenterJsonPath);
                    var costcenters = JsonSerializer.Deserialize<List<CostCenter>> (costcenterData);

                    var assetJsonPath = Path.Combine(AppContext.BaseDirectory, "Setup", "assets.json");
                    var assetData = File.ReadAllText (assetJsonPath);
                    var assets = JsonSerializer.Deserialize<List<Asset>> (assetData);
                   
                    if (licenses != null)
                    {
                        context.Set<License>().RemoveRange(context.Set<License>());
                        context.Set<License>().AddRange(licenses);
                        context.SaveChanges();
                    }
                    if (tenants != null)
                    {
                        context.Set<Tenant>().RemoveRange(context.Set<Tenant>());
                        context.Set<Tenant>().AddRange(tenants);
                        context.SaveChanges();
                    }
                    if (countries != null)
                    {
                        context.Set<Country>().RemoveRange(context.Set<Country>());
                        context.Set<Country>().AddRange(countries);
                        context.SaveChanges();
                    }
                    if (companies != null)
                    {
                        context.Set<Company>().RemoveRange(context.Set<Company>());
                        context.Set<Company>().AddRange(companies);
                        context.SaveChanges();
                    }
                   if (organizations != null)
                    {
                        context.Set<Organization>().RemoveRange(context.Set<Organization>());
                        context.Set<Organization>().AddRange(organizations);
                        context.SaveChanges();
                    }
                   if (locations != null)
                    {
                        context.Set<Location>().RemoveRange(context.Set<Location>());
                        context.Set<Location>().AddRange(locations);
                        context.SaveChanges();
                    }
                    if (manufacturers != null)
                    {
                        context.Set<Manufacturer>().RemoveRange(context.Set<Manufacturer>());
                        context.Set<Manufacturer>().AddRange(manufacturers);
                        context.SaveChanges();
                    }
                    if (costcenters != null)
                    {
                            context.Set<CostCenter>().RemoveRange(context.Set<CostCenter>());
                            context.Set<CostCenter>().AddRange(costcenters);
                            context.SaveChanges();
                    }
                    if (assets != null)
                    {
                            context.Set<Asset>().RemoveRange(context.Set<Asset>());
                            context.Set<Asset>().AddRange(assets);
                            context.SaveChanges();
                    }
                    


                });
            });
            
        
        }
}

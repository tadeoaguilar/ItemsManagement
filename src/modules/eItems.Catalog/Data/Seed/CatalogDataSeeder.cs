
using System.Text.Json;
using eItems.Shared.Data.Seed;

namespace eItems.Catalog.Data.Seed;
public class CatalogDataSeeder(CatalogContext context)
    : IDataSeeder
{
    public async Task SeedAllAsync()
    {
                           try{                       // Enable detailed errors and logging

                    await context.Database.EnsureCreatedAsync();
                    var licenseJsonPath = Path.Combine(AppContext.BaseDirectory, "Setup", "licenses.json");
                    Console.WriteLine($"Looking for seed data at: {licenseJsonPath}");
                    Console.WriteLine($"Setup folder path: {licenseJsonPath}");
                    Console.WriteLine($"Setup folder exists: {Directory.Exists(licenseJsonPath)}");
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
                        await context.Set<License>().AddRangeAsync(licenses);
                        await context.SaveChangesAsync();
                    }
                    if (tenants != null)
                    {
                        context.Set<Tenant>().RemoveRange(context.Set<Tenant>());
                        await context.Set<Tenant>().AddRangeAsync(tenants);
                        await context.SaveChangesAsync();
                    }
                    if (countries != null)
                    {
                        context.Set<Country>().RemoveRange(context.Set<Country>());
                        await context.Set<Country>().AddRangeAsync(countries);
                        await context.SaveChangesAsync();
                    }
                    if (companies != null)
                    {
                        context.Set<Company>().RemoveRange(context.Set<Company>());
                        await context.Set<Company>().AddRangeAsync(companies);
                        await context.SaveChangesAsync();
                    }
                   if (organizations != null)
                    {
                        context.Set<Organization>().RemoveRange(context.Set<Organization>());
                        await context.Set<Organization>().AddRangeAsync(organizations);
                        await context.SaveChangesAsync();
                    }
                   if (locations != null)
                    {
                        context.Set<Location>().RemoveRange(context.Set<Location>());
                        await context.Set<Location>().AddRangeAsync(locations);
                        await context.SaveChangesAsync();
                    }
                    if (manufacturers != null)
                    {
                        context.Set<Manufacturer>().RemoveRange(context.Set<Manufacturer>());
                        await context.Set<Manufacturer>().AddRangeAsync(manufacturers);
                        await context.SaveChangesAsync();
                    }
                    if (costcenters != null)
                    {
                            context.Set<CostCenter>().RemoveRange(context.Set<CostCenter>());
                            await context.Set<CostCenter>().AddRangeAsync(costcenters);
                            await context.SaveChangesAsync();context.SaveChanges();
                    }
                    if (assets != null)
                    {
                            context.Set<Asset>().RemoveRange(context.Set<Asset>());
                            await context.Set<Asset>().AddRangeAsync(assets);
                            await context.SaveChangesAsync();
                    }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error seeding database: {ex.Message}");
                        throw;
                    }
    }
}

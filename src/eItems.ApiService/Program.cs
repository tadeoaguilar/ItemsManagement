using System.Text.Json;
using eItems.Catalog;
using eItems.Catalog.Data;
using eItems.Catalog.Data.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var catalogAssembly = typeof(CatalogModule).Assembly;
// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddNpgsqlDbContext<CatalogContext>(connectionName:"eItems" ) ;
builder.Services.AddCatalogModule(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}




// Minimal API to fetch data from eHost.Catalog DB
app.MapGet("/api/tenants", async (CatalogContext dbContext) =>
{
    var tenants = await dbContext.Tenant.ToListAsync();
    return Results.Ok(tenants);
})
.WithName("GetTenants");

app.MapDefaultEndpoints();
app.UseCatalogModule();
app.Run();


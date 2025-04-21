using System.Text.Json;
using eItems.Catalog;
using eItems.Catalog.Data;
using eItems.Catalog.Data.Model;
using eItems.Catalog.Data.Model.Assets;
using Microsoft.EntityFrameworkCore;
using eItems.Shared.Extensions;
var builder = WebApplication.CreateBuilder(args);
var catalogAssembly = typeof(CatalogModule).Assembly;
// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Add MediatR services
builder.Services.AddMediatRWithAssemblies(catalogAssembly);

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
app.MapCatalogApi();
app.MapDefaultEndpoints();

app.UseCatalogModule();

app.Run();


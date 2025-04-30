using System.Text.Json;
using eItems.Catalog;
using eItems.Catalog.Data;
using eItems.Catalog.Data.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using eItems.Shared.Extensions;
using eItems.Catalog.Apis;
using eItems.Identity.Data;
using eItems.Identity.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using eItems.Identity;
using eItems.Identity.Services;
var builder = WebApplication.CreateBuilder(args);
var catalogAssembly = typeof(CatalogModule).Assembly;
// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();



builder.AddNpgsqlDbContext<IdentityAppDbContext>(connectionName:"eItems" ) ;

// Register AuthenticationService
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityAppDbContext>()
    .AddDefaultTokenProviders();

// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
    };
});
// Add Authorization services
builder.Services.AddAuthorization();
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

app.UseIdentityModule();
app.UseAuthentication();
app.UseAuthorization();
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
app.MapAssetApi();
app.MapCompanyApi();
app.MapDefaultEndpoints();

app.UseCatalogModule();

app.Run();


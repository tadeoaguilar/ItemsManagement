using System;
using eItems.Catalog.Data.Model;
using eItems.Catalog.Data.Model.Companies.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace eItems.Catalog.Apis;

public static class CompanyApi
{
    public static IEndpointRouteBuilder MapCompanyApi(this IEndpointRouteBuilder app)
    {
        app.MapPost("/companies", CreateCompany)
           .WithName("CreateCompany")
           .ProducesProblem(StatusCodes.Status400BadRequest)
           .WithSummary("Create Company")
           .WithDescription("Create a new company");

        app.MapGet("/companies", GetCompanies)
           .WithName("GetCompanies")
           .WithSummary("Get Companies")
           .WithDescription("Get all companies");

        app.MapGet("/companies/{id}", GetCompanyById)
           .WithName("GetCompanyById")
           .ProducesProblem(StatusCodes.Status404NotFound)
           .WithSummary("Get Company by ID")
           .WithDescription("Get a company by its ID");

        app.MapPut("/companies/{id}", UpdateCompany)
           .WithName("UpdateCompany")
           .ProducesProblem(StatusCodes.Status404NotFound)
           .WithSummary("Update Company")
           .WithDescription("Update an existing company");

        app.MapDelete("/companies/{id}", DeleteCompany)
           .WithName("DeleteCompany")
           .ProducesProblem(StatusCodes.Status404NotFound)
           .WithSummary("Delete Company")
           .WithDescription("Delete a company");

        return app;
    }

    private static async Task<IResult> CreateCompany(CompanyDto companyDto, CatalogContext context)
    {
        var company = new Company
        {
            Id = Guid.NewGuid(),
            TenantID = companyDto.TenantID,
            CountryID = companyDto.CountryID,
            CompanyCD = companyDto.CompanyCD,
            Description = companyDto.Description,
            Active = companyDto.Active
        };

        await context.Company.AddAsync(company);
        await context.SaveChangesAsync();
        return Results.Created($"/companies/{company.Id}", company.Id);
    }

    private static async Task<IResult> GetCompanies(CatalogContext context)
    {
        var companies = await context.Company
            .Include(c => c.Tenant)
            .Include(c => c.Country)
            .Select(c => new CompanyResponseDto
            {
                Id = c.Id,
                CompanyCD = c.CompanyCD,
                Description = c.Description,
                Active = c.Active,
                TenantName = c.Tenant.Description,
                CountryName = c.Country.Description
            })
            .ToListAsync();
        return Results.Ok(companies);
    }

    private static async Task<IResult> GetCompanyById(Guid id, CatalogContext context)
    {
        var company = await context.Company
            .Include(c => c.Tenant)
            .Include(c => c.Country)
            .Select(c => new CompanyResponseDto
            {
                Id = c.Id,
                CompanyCD = c.CompanyCD,
                Description = c.Description,
                Active = c.Active,
                TenantName = c.Tenant.Description,
                CountryName = c.Country.Description
            })
            .FirstOrDefaultAsync(c => c.Id == id);

        return company is null ? Results.NotFound() : Results.Ok(company);
    }

    private static async Task<IResult> UpdateCompany(Guid id, CompanyDto companyDto, CatalogContext context)
    {
        var company = await context.Company.FindAsync(id);
        if (company is null) return Results.NotFound();

        company.TenantID = companyDto.TenantID;
        company.CountryID = companyDto.CountryID;
        company.CompanyCD = companyDto.CompanyCD;
        company.Description = companyDto.Description;
        company.Active = companyDto.Active;

        await context.SaveChangesAsync();
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteCompany(Guid id, CatalogContext context)
    {
        var company = await context.Company.FindAsync(id);
        if (company is null) return Results.NotFound();

        context.Company.Remove(company);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }
}
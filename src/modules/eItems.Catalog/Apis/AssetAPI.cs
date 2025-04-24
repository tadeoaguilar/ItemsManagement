using System;
using eItems.Catalog.Data.Model.Assets.Dto;
using eItems.Catalog.Data.Model.Companies.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace eItems.Catalog.Apis;

public static class AssetApi
{
    public static IEndpointRouteBuilder MapAssetApi(this IEndpointRouteBuilder app)
    {
        // Register all endpoints
        app.MapPost("/assets", CreateAsset)
           .WithName("CreateAsset")
           .ProducesProblem(StatusCodes.Status400BadRequest)
           .WithSummary("Create Asset")
           .WithDescription("Create a new asset");

        app.MapGet("/assets", GetAssets)
           .WithName("GetAssets")
           .WithSummary("Get Assets")
           .WithDescription("Get all assets");

        app.MapGet("/assets/{id}", GetAssetById)
           .WithName("GetAssetById")
           .ProducesProblem(StatusCodes.Status404NotFound)
           .WithSummary("Get Asset by ID")
           .WithDescription("Get an asset by its ID");

        app.MapPut("/assets/{id}", UpdateAsset)
           .WithName("UpdateAsset")
           .ProducesProblem(StatusCodes.Status404NotFound)
           .WithSummary("Update Asset")
           .WithDescription("Update an existing asset");

        app.MapDelete("/assets/{id}", DeleteAsset)
           .WithName("DeleteAsset")
           .ProducesProblem(StatusCodes.Status404NotFound)
           .WithSummary("Delete Asset")
           .WithDescription("Delete an asset");

        return app;
    }

    private static async Task<IResult> CreateAsset(AssetDto assetDto, CatalogContext context)
    {
        var result = await context.Asset.AddAsync(Asset.Create(
            Guid.NewGuid(),
            assetDto.LocationID,
            assetDto.ManufacturerID,
            assetDto.CostCenterID,
            assetDto.AssetCD,
            assetDto.AssetImage,
            assetDto.SubNumber,
            assetDto.Description,
            assetDto.Active
        ));
        
        await context.SaveChangesAsync();
        return Results.Created($"/assets/{result.Entity.Id}", result.Entity.Id);
    }

    private static async Task<IResult> GetAssets(CatalogContext context)
    {
        var assets = await context.Asset
            .Include(a => a.Location)
            .Include(a => a.Manufacturer)
            .Include(a => a.CostCenter)
            .Select(a => new AssetResponseDto
            {
                Id = a.Id,
                AssetCD = a.AssetCD,
                AssetImage = a.AssetImage,
                SubNumber = a.SubNumber,
                Description = a.Description,
                Active = a.Active,
                LocationName = a.Location.Description,
                ManufacturerName = a.Manufacturer.Description,
                CostCenterName = a.CostCenter.Description
            })
            .ToListAsync();
        
        return Results.Ok(assets);
    }

    private static async Task<IResult> GetAssetById(Guid id, CatalogContext context)
    {
        var asset = await context.Asset
            .Include(a => a.Location)
            .Include(a => a.Manufacturer)
            .Include(a => a.CostCenter)
            .Select(a => new AssetResponseDto
            {
                Id = a.Id,
                AssetCD = a.AssetCD,
                AssetImage = a.AssetImage,
                SubNumber = a.SubNumber,
                Description = a.Description,
                Active = a.Active,
                LocationName = a.Location.Description,
                ManufacturerName = a.Manufacturer.Description,
                CostCenterName = a.CostCenter.Description
            })
            .FirstOrDefaultAsync(a => a.Id == id);

        return asset is null ? Results.NotFound() : Results.Ok(asset);
    }

    private static async Task<IResult> UpdateAsset(Guid id, AssetDto assetDto, CatalogContext context)
    {
        var asset = await context.Asset.FindAsync(id);
        if (asset is null) return Results.NotFound();

        asset.LocationID = assetDto.LocationID;
        asset.ManufacturerID = assetDto.ManufacturerID;
        asset.CostCenterID = assetDto.CostCenterID;
        asset.AssetCD = assetDto.AssetCD;
        asset.AssetImage = assetDto.AssetImage;
        asset.SubNumber = assetDto.SubNumber;
        asset.Description = assetDto.Description;
        asset.Active = assetDto.Active;

        await context.SaveChangesAsync();
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsset(Guid id, CatalogContext context)
    {
        var asset = await context.Asset.FindAsync(id);
        if (asset is null) return Results.NotFound();

        context.Asset.Remove(asset);
        await context.SaveChangesAsync();
        return Results.NoContent();
    }
}
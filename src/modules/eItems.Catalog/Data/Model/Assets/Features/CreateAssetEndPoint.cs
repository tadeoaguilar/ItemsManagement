using System;
using eItems.Catalog.Data.Model.Dto;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace eItems.Catalog.Data.Model.Assets;

public record CreateAssetRequest(AssetDto Asset);
public record CreateAssetResponse(Guid Id);

public static class CreateAssetEndpoint 
{
    public static  IEndpointRouteBuilder MapCatalogApi(this IEndpointRouteBuilder app)
    {

        app.MapPost("/assets", async (CreateAssetRequest request, ISender sender) =>
        {
            //var command = request.Adapt<CreateAssetCommand>();

            var result = await sender.Send(new CreateAssetCommand(request.Asset));

            var response = new CreateAssetResponse(result.Id);


            return Results.Created($"/assets/{response.Id}", response);
        })
        .WithName("CreateAsset")
        .Produces<CreateAssetResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Asset")
        .WithDescription("Create Asset");

        app.MapGet("/hola", async (ISender sender) =>
        {
            var result = "Hola Mundo";

            return Results.Ok(result);
        })
        .WithName("Hola")        
        .WithSummary("Hola")
        .WithDescription("Create Asset");

        return app;
    }
}
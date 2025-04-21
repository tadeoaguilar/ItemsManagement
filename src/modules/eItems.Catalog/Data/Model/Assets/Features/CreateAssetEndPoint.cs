using System;
using eItems.Catalog.Data.Model.Dto;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace eItems.Catalog.Data.Model.Assets;

public record CreateAssetRequest(AssetDto Asset);
public record CreateAssetResponse(Guid Id);

public class CreateAssetEndpoint 
{
    public void AddRoutes(Microsoft.AspNetCore.Routing.IEndpointRouteBuilder app)
    {
        app.MapPost("/assets1", async (CreateAssetRequest request, ISender sender) =>
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
    }
}
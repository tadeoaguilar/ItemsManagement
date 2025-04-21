using System;
using eItems.Catalog.Data.Model.Dto;
using FluentValidation;
using Shared.Contracts.CQRS;

namespace eItems.Catalog.Data.Model.Assets;



public record CreateAssetCommand(AssetDto Asset)
    : ICommand<CreateAssetResult>;
public record CreateAssetResult(Guid Id);
public class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
{
    public CreateAssetCommandValidator()
    {
        RuleFor(x => x.Asset.Description).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Asset.AssetCD).NotEmpty().WithMessage("Asset CD is required");
        RuleFor(x => x.Asset.AssetImage).NotEmpty().WithMessage("ImageFile is required");
        
    }
}


public class CreateAssetHandler
    (CatalogContext context)
    : ICommandHandler<CreateAssetCommand, CreateAssetResult>
{
    public async Task<CreateAssetResult> Handle(CreateAssetCommand command, CancellationToken cancellationToken)
    {
        var asset = CreateNewAsset(command.Asset);
        context.Asset.Add(asset);
        await context.SaveChangesAsync(cancellationToken);

        return new CreateAssetResult(asset.Id);
    }

    private Asset CreateNewAsset(AssetDto assetDto)
    {
        var product = Asset.Create(
            Guid.NewGuid(),
            assetDto.LocationID,
            assetDto.ManufacturerID,
            assetDto.CostCenterID,
            assetDto.AssetCD,
            assetDto.AssetImage,
            assetDto.SubNumber,
            assetDto.Description,
            assetDto.Active            
            );

        return product;
    }
}

using FluentValidation;

namespace eItems.Catalog.Application.Commands.Assets;

public class UpdateAssetCommandValidator : AbstractValidator<UpdateAssetCommand>
{
    public UpdateAssetCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Asset ID is required");

        RuleFor(x => x.LocationID)
            .NotEmpty()
            .WithMessage("Location ID is required");

        RuleFor(x => x.ManufacturerID)
            .NotEmpty()
            .WithMessage("Manufacturer ID is required");

        RuleFor(x => x.CostCenterID)
            .NotEmpty()
            .WithMessage("Cost Center ID is required");

        RuleFor(x => x.AssetCD)
            .NotEmpty()
            .WithMessage("Asset Code is required")
            .MaximumLength(50)
            .WithMessage("Asset Code must not exceed 50 characters");

        RuleFor(x => x.AssetImage)
            .NotEmpty()
            .WithMessage("Asset Image is required")
            .MaximumLength(200)
            .WithMessage("Asset Image path must not exceed 200 characters");

        RuleFor(x => x.SubNumber)
            .GreaterThan(0)
            .WithMessage("Sub Number must be greater than 0");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters");
    }
}
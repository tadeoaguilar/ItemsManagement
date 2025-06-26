using Shared.Contracts.CQRS;

namespace eItems.Catalog.Application.Commands.Assets;

public record UpdateAssetCommand(
    Guid Id,
    Guid LocationID,
    Guid ManufacturerID,
    Guid CostCenterID,
    string AssetCD,
    string AssetImage,
    int SubNumber,
    string? Description,
    bool Active
) : ICommand;
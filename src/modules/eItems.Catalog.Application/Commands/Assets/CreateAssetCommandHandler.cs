using Shared.Contracts.CQRS;
using eItems.Catalog.Domain.Repositories;
using eItems.Catalog.Data.Model;

namespace eItems.Catalog.Application.Commands.Assets;

public class CreateAssetCommandHandler : ICommandHandler<CreateAssetCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAssetCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = Asset.Create(
            Guid.NewGuid(),
            request.LocationID,
            request.ManufacturerID,
            request.CostCenterID,
            request.AssetCD,
            request.AssetImage,
            request.SubNumber,
            request.Description,
            request.Active
        );

        await _unitOfWork.Assets.AddAsync(asset, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return asset.Id;
    }
}
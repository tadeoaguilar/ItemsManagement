using MediatR;
using Shared.Contracts.CQRS;
using eItems.Catalog.Domain.Repositories;

namespace eItems.Catalog.Application.Commands.Assets;

public class UpdateAssetCommandHandler : ICommandHandler<UpdateAssetCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAssetCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = await _unitOfWork.Assets.GetByIdAsync(request.Id, cancellationToken);
        
        if (asset == null)
        {
            throw new InvalidOperationException($"Asset with ID {request.Id} not found");
        }

        // Update asset properties
        asset.LocationID = request.LocationID;
        asset.ManufacturerID = request.ManufacturerID;
        asset.CostCenterID = request.CostCenterID;
        asset.AssetCD = request.AssetCD;
        asset.AssetImage = request.AssetImage;
        asset.SubNumber = request.SubNumber;
        asset.Description = request.Description;
        asset.Active = request.Active;

        await _unitOfWork.Assets.UpdateAsync(asset, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
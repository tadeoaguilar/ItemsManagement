using MediatR;
using Shared.Contracts.CQRS;
using eItems.Catalog.Domain.Repositories;

namespace eItems.Catalog.Application.Commands.Assets;

public class DeleteAssetCommandHandler : ICommandHandler<DeleteAssetCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAssetCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
    {
        var exists = await _unitOfWork.Assets.ExistsAsync(request.Id, cancellationToken);
        
        if (!exists)
        {
            throw new InvalidOperationException($"Asset with ID {request.Id} not found");
        }

        await _unitOfWork.Assets.DeleteAsync(request.Id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
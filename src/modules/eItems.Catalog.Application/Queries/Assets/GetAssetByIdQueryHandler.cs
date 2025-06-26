using Shared.Contracts.CQRS;
using eItems.Catalog.Domain.Repositories;
using eItems.Catalog.Data.Model.Assets.Dto;
using eItems.Catalog.Application.Common;

namespace eItems.Catalog.Application.Queries.Assets;

public class GetAssetByIdQueryHandler : IQueryHandler<GetAssetByIdQuery, Result<AssetResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAssetByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AssetResponseDto>> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
    {
        var asset = await _unitOfWork.Assets.GetByIdAsync(request.Id, cancellationToken);
        
        if (asset == null)
        {
            return Result<AssetResponseDto>.NotFound();
        }

        var response = new AssetResponseDto
        {
            Id = asset.Id,
            AssetCD = asset.AssetCD,
            AssetImage = asset.AssetImage,
            SubNumber = asset.SubNumber,
            Description = asset.Description,
            Active = asset.Active,
            LocationName = asset.Location?.Description,
            ManufacturerName = asset.Manufacturer?.Description,
            CostCenterName = asset.CostCenter?.Description
        };

        return Result<AssetResponseDto>.Success(response);
    }
}
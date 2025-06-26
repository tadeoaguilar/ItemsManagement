using Shared.Contracts.CQRS;
using eItems.Catalog.Domain.Repositories;
using eItems.Catalog.Data.Model;
using eItems.Catalog.Data.Model.Assets.Dto;

namespace eItems.Catalog.Application.Queries.Assets;

public class GetAssetsPaginatedQueryHandler : IQueryHandler<GetAssetsPaginatedQuery, PaginatedItems<AssetResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAssetsPaginatedQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedItems<AssetResponseDto>> Handle(GetAssetsPaginatedQuery request, CancellationToken cancellationToken)
    {
        var paginationRequest = new PaginationRequest(request.PageSize, request.PageIndex);
        var paginatedAssets = await _unitOfWork.Assets.GetPaginatedAsync(paginationRequest, cancellationToken);

        var assetDtos = paginatedAssets.Data.Select(asset => new AssetResponseDto
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
        });

        return new PaginatedItems<AssetResponseDto>(
            paginatedAssets.PageIndex,
            paginatedAssets.PageSize,
            paginatedAssets.Count,
            assetDtos
        );
    }
}
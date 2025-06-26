using Shared.Contracts.CQRS;
using eItems.Catalog.Data.Model;
using eItems.Catalog.Data.Model.Assets.Dto;

namespace eItems.Catalog.Application.Queries.Assets;

public record GetAssetsPaginatedQuery(
    int PageSize = 10,
    int PageIndex = 0
) : IQuery<PaginatedItems<AssetResponseDto>>;
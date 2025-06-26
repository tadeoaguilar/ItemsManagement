using Shared.Contracts.CQRS;
using eItems.Catalog.Data.Model.Assets.Dto;
using eItems.Catalog.Application.Common;

namespace eItems.Catalog.Application.Queries.Assets;

public record GetAssetByIdQuery(Guid Id) : IQuery<Result<AssetResponseDto>>;
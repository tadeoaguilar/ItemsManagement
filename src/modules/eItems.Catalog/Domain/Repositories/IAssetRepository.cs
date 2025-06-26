using eItems.Catalog.Data.Model.Assets;
using eItems.Catalog.Data.Model;

namespace eItems.Catalog.Domain.Repositories;

public interface IAssetRepository
{
    Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Asset>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PaginatedItems<Asset>> GetPaginatedAsync(PaginationRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<Asset>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default);
    Task<Asset> AddAsync(Asset asset, CancellationToken cancellationToken = default);
    Task<Asset> UpdateAsync(Asset asset, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
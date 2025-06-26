using eItems.Catalog.Data.Model;

namespace eItems.Catalog.Domain.Repositories;

public interface ICompanyRepository
{
    Task<Company?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Company>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PaginatedItems<Company>> GetPaginatedAsync(PaginationRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<Company>> GetByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default);
    Task<Company> AddAsync(Company company, CancellationToken cancellationToken = default);
    Task<Company> UpdateAsync(Company company, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameAsync(string name, Guid tenantId, CancellationToken cancellationToken = default);
}
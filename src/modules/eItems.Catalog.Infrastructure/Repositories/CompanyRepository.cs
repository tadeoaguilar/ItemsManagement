using Microsoft.EntityFrameworkCore;
using eItems.Catalog.Data;
using eItems.Catalog.Data.Model;
using eItems.Catalog.Domain.Repositories;

namespace eItems.Catalog.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly CatalogContext _context;

    public CompanyRepository(CatalogContext context)
    {
        _context = context;
    }

    public async Task<Company?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Company
            .Include(c => c.Tenant)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Company>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Company
            .Include(c => c.Tenant)
            .ToListAsync(cancellationToken);
    }

    public async Task<PaginatedItems<Company>> GetPaginatedAsync(PaginationRequest request, CancellationToken cancellationToken = default)
    {
        var totalCount = await _context.Company.LongCountAsync(cancellationToken);
        
        var companies = await _context.Company
            .Include(c => c.Tenant)
            .Skip(request.PageIndex * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedItems<Company>(request.PageIndex, request.PageSize, totalCount, companies);
    }

    public async Task<IEnumerable<Company>> GetByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default)
    {
        return await _context.Company
            .Include(c => c.Tenant)
            .Where(c => c.TenantID == tenantId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Company> AddAsync(Company company, CancellationToken cancellationToken = default)
    {
        _context.Company.Add(company);
        return company;
    }

    public async Task<Company> UpdateAsync(Company company, CancellationToken cancellationToken = default)
    {
        _context.Company.Update(company);
        return company;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var company = await _context.Company.FindAsync(new object[] { id }, cancellationToken);
        if (company != null)
        {
            _context.Company.Remove(company);
        }
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Company.AnyAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, Guid tenantId, CancellationToken cancellationToken = default)
    {
        return await _context.Company.AnyAsync(c => c.CompanyCD == name && c.TenantID == tenantId, cancellationToken);
    }
}
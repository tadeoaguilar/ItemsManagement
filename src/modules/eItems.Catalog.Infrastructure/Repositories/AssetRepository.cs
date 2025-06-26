using Microsoft.EntityFrameworkCore;
using eItems.Catalog.Data;
using eItems.Catalog.Data.Model.Assets;
using eItems.Catalog.Data.Model;
using eItems.Catalog.Domain.Repositories;

namespace eItems.Catalog.Infrastructure.Repositories;

public class AssetRepository : IAssetRepository
{
    private readonly CatalogContext _context;

    public AssetRepository(CatalogContext context)
    {
        _context = context;
    }

    public async Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Asset
            .Include(a => a.Location)
            .Include(a => a.Manufacturer)
            .Include(a => a.CostCenter)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Asset>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Asset
            .Include(a => a.Location)
            .Include(a => a.Manufacturer)
            .Include(a => a.CostCenter)
            .ToListAsync(cancellationToken);
    }

    public async Task<PaginatedItems<Asset>> GetPaginatedAsync(PaginationRequest request, CancellationToken cancellationToken = default)
    {
        var totalCount = await _context.Asset.LongCountAsync(cancellationToken);
        
        var assets = await _context.Asset
            .Include(a => a.Location)
            .Include(a => a.Manufacturer)
            .Include(a => a.CostCenter)
            .Skip(request.PageIndex * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedItems<Asset>(request.PageIndex, request.PageSize, totalCount, assets);
    }

    public async Task<IEnumerable<Asset>> GetByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken = default)
    {
        return await _context.Asset
            .Include(a => a.Location)
            .Include(a => a.Manufacturer)
            .Include(a => a.CostCenter)
            .Where(a => a.LocationID == companyId) // Note: Using LocationID as proxy for company filter
            .ToListAsync(cancellationToken);
    }

    public async Task<Asset> AddAsync(Asset asset, CancellationToken cancellationToken = default)
    {
        _context.Asset.Add(asset);
        return asset;
    }

    public async Task<Asset> UpdateAsync(Asset asset, CancellationToken cancellationToken = default)
    {
        _context.Asset.Update(asset);
        return asset;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var asset = await _context.Asset.FindAsync(new object[] { id }, cancellationToken);
        if (asset != null)
        {
            _context.Asset.Remove(asset);
        }
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Asset.AnyAsync(a => a.Id == id, cancellationToken);
    }
}
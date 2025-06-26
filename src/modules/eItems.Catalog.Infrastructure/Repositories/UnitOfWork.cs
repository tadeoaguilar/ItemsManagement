using Microsoft.EntityFrameworkCore.Storage;
using eItems.Catalog.Data;
using eItems.Catalog.Domain.Repositories;

namespace eItems.Catalog.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CatalogContext _context;
    private IDbContextTransaction? _transaction;
    private bool _disposed = false;

    public UnitOfWork(CatalogContext context)
    {
        _context = context;
        Assets = new AssetRepository(_context);
        Companies = new CompanyRepository(_context);
    }

    public IAssetRepository Assets { get; }
    public ICompanyRepository Companies { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _transaction?.Dispose();
            _context.Dispose();
            _disposed = true;
        }
    }
}
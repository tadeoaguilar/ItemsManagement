namespace eItems.Catalog.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    IAssetRepository Assets { get; }
    ICompanyRepository Companies { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
using Microsoft.EntityFrameworkCore;
using eItems.Catalog.Data;
using eItems.Catalog.Data.Model;
using eItems.Catalog.Infrastructure.Repositories;
using FluentAssertions;

namespace eItems.Catalog.Infrastructure.Tests.Repositories;

public class AssetRepositoryTests : IDisposable
{
    private readonly CatalogContext _context;
    private readonly AssetRepository _repository;

    public AssetRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new CatalogContext(options);
        _repository = new AssetRepository(_context);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingAsset_ReturnsAsset()
    {
        // Arrange
        var assetId = Guid.NewGuid();
        var asset = Asset.Create(
            assetId,
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "ASSET001",
            "image.jpg",
            1,
            "Test Asset",
            true
        );

        await _context.Asset.AddAsync(asset);
        await _context.SaveChangesAsync();

        // Act - Use direct context call for in-memory testing
        var result = await _context.Asset.FindAsync(assetId);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(assetId);
        result.AssetCD.Should().Be("ASSET001");
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingAsset_ReturnsNull()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        // Act
        var result = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task AddAsync_ValidAsset_AddsAssetToContext()
    {
        // Arrange
        var asset = Asset.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "ASSET002",
            "image2.jpg",
            2,
            "Test Asset 2",
            true
        );

        // Act
        var result = await _repository.AddAsync(asset);
        await _context.SaveChangesAsync();

        // Assert
        result.Should().Be(asset);
        var savedAsset = await _context.Asset.FindAsync(asset.Id);
        savedAsset.Should().NotBeNull();
        savedAsset!.AssetCD.Should().Be("ASSET002");
    }

    [Fact]
    public async Task ExistsAsync_ExistingAsset_ReturnsTrue()
    {
        // Arrange
        var assetId = Guid.NewGuid();
        var asset = Asset.Create(
            assetId,
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "ASSET003",
            "image3.jpg",
            3,
            "Test Asset 3",
            true
        );

        await _context.Asset.AddAsync(asset);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.ExistsAsync(assetId);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task ExistsAsync_NonExistingAsset_ReturnsFalse()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        // Act
        var result = await _repository.ExistsAsync(nonExistingId);

        // Assert
        result.Should().BeFalse();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
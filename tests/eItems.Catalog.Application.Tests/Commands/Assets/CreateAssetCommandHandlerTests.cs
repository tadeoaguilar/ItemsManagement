using eItems.Catalog.Application.Commands.Assets;
using eItems.Catalog.Domain.Repositories;
using eItems.Catalog.Data.Model;
using Moq;
using FluentAssertions;

namespace eItems.Catalog.Application.Tests.Commands.Assets;

public class CreateAssetCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IAssetRepository> _assetRepositoryMock;
    private readonly CreateAssetCommandHandler _handler;

    public CreateAssetCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _assetRepositoryMock = new Mock<IAssetRepository>();
        _unitOfWorkMock.Setup(x => x.Assets).Returns(_assetRepositoryMock.Object);
        _handler = new CreateAssetCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ReturnsAssetId()
    {
        // Arrange
        var command = new CreateAssetCommand(
            LocationID: Guid.NewGuid(),
            ManufacturerID: Guid.NewGuid(),
            CostCenterID: Guid.NewGuid(),
            AssetCD: "ASSET001",
            AssetImage: "image.jpg",
            SubNumber: 1,
            Description: "Test Asset",
            Active: true
        );

        _assetRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Asset>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync((Asset asset, CancellationToken _) => asset);

        _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        _assetRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Asset>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ValidCommand_CreatesAssetWithCorrectProperties()
    {
        // Arrange
        var locationId = Guid.NewGuid();
        var manufacturerId = Guid.NewGuid();
        var costCenterId = Guid.NewGuid();
        var command = new CreateAssetCommand(
            LocationID: locationId,
            ManufacturerID: manufacturerId,
            CostCenterID: costCenterId,
            AssetCD: "ASSET001",
            AssetImage: "image.jpg",
            SubNumber: 1,
            Description: "Test Asset",
            Active: true
        );

        Asset? capturedAsset = null;
        _assetRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Asset>(), It.IsAny<CancellationToken>()))
                          .Callback<Asset, CancellationToken>((asset, _) => capturedAsset = asset)
                          .ReturnsAsync((Asset asset, CancellationToken _) => asset);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        capturedAsset.Should().NotBeNull();
        capturedAsset!.LocationID.Should().Be(locationId);
        capturedAsset.ManufacturerID.Should().Be(manufacturerId);
        capturedAsset.CostCenterID.Should().Be(costCenterId);
        capturedAsset.AssetCD.Should().Be("ASSET001");
        capturedAsset.AssetImage.Should().Be("image.jpg");
        capturedAsset.SubNumber.Should().Be(1);
        capturedAsset.Description.Should().Be("Test Asset");
        capturedAsset.Active.Should().BeTrue();
    }
}
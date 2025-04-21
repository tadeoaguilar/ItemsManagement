// filepath: c:\gitlab\ItemsManagement\src\modules\eItems.Catalog\Data\Model\Assets\Dto\AssetDto.cs
namespace eItems.Catalog.Data.Model.Dto
{
    public record AssetDto
    (
        Guid Id,
        Guid LocationID,
        Guid ManufacturerID,
        Guid CostCenterID,
        string AssetCD,
        string AssetImage,
        int SubNumber,
        string? Description,
        bool Active
    );
}

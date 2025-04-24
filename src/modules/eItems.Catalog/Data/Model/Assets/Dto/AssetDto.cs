using System;

namespace eItems.Catalog.Data.Model.Assets.Dto;

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

public record AssetResponseDto
    {
        public Guid Id { get; set; }
        public string AssetCD { get; set; } = default!;
        public string AssetImage { get; set; } = default!;
        public int SubNumber { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        
        // Include only needed properties from navigation entities
        public string? LocationName { get; set; }
        public string? ManufacturerName { get; set; }
        public string? CostCenterName { get; set; }
    }

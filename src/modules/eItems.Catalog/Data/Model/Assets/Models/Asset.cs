



namespace eItems.Catalog.Data.Model
{
    public class Asset : Entity<Guid>
    {

        public Location Location { get; set; } = default!;
        public Guid LocationID { get; set; } = default!;

        public Manufacturer Manufacturer { get; set; } = default!;
        public Guid ManufacturerID { get; set; } = default!;
        public CostCenter CostCenter { get; set; } = default!;
        public Guid CostCenterID { get; set; } = default!;
        public string AssetCD { get; set; } = default!;
        public string AssetImage { get; set; } = default!;
        public int SubNumber { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public bool Active { get; set; } = default!;
        public ICollection<LanguageDescr> LanguageDescr { get; } = new List<LanguageDescr>();

        public static Asset Create(
            Guid id,     
            Guid locationID,     
            Guid manufacturerID,     
            Guid costCenterID,
            string assetCD,
            string assetImage,
            int subNumber,
            string? description,
            bool active)
        {
            return new Asset
            {
                Id = id,             
                LocationID = locationID,             
                ManufacturerID = manufacturerID,             
                CostCenterID = costCenterID,
                AssetCD = assetCD,
                AssetImage = assetImage,
                SubNumber = subNumber,
                Description = description,
                Active = active
            };
        }


    }
}

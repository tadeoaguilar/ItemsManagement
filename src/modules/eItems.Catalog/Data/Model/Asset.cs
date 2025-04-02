



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
        public int SubNumber { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public bool Active { get; set; } = default!;
        public ICollection<LanguageDescr> LanguageDescr { get; } = new List<LanguageDescr>();

    }
}

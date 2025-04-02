namespace eItems.Catalog.Data.Model
{
    public class Organization : Entity<Guid>
    {
        public Company Company { get; set; } = default!;
        public Guid CompanyID { get; set; } = default!;
        public string CompanyCD { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public bool Active { get; set; } = default!;

        public ICollection<Location> Locations { get; } = new List<Location>();
        public ICollection<Asset> Assets { get; } = new List<Asset>();
        //public ICollection<Asset> Assets { get; } = new List<Asset>();
    }
}



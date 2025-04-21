namespace eItems.Catalog.Data.Model
{
    public class Location : Entity<Guid>
    {


        public Organization Organization { get; set; } = default!;
        public Guid OrganizationID { get; set; } = default!;
        public string LocationCD { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public bool Active { get; set; } = default!;
        public ICollection<Asset> Assets { get; } = new List<Asset>();
    }
}

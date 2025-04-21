namespace eItems.Catalog.Data.Model
{
    public class Manufacturer : Entity<Guid>
    {

        public string ManufacturerCD { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public bool Active { get; set; } = default!;

        public ICollection<Asset> Assets { get; } = new List<Asset>();

    }

}



namespace eItems.Catalog.Data.Model
{
    public class Country : Entity<Guid>
    {

        public string CountryCD { get; set; } = default!;
        public string? Description { get; set; } = default!;

        public ICollection<Company> Companies { get; } = new List<Company>();
    }
}

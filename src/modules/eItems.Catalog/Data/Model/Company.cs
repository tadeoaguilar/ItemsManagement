

namespace eItems.Catalog.Data.Model
{
    public class Company : Entity<Guid>
    {
        public Tenant Tenant { get; set; } = default!;
        public Country Country { get; set; } = default!;
        public Guid TenantID { get; set; } = default!;
        public Guid CountryID { get; set; } = default!;
        public string CompanyCD { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public bool Active { get; set; } = default!;

        public ICollection<Organization> Organizations { get; } = new List<Organization>();
    }
}

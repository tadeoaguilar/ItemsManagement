
namespace eItems.Catalog.Data.Model
{
    public class Tenant : Entity<Guid>
    {
        public string TenantName { get; set; } = default!;
        public string TenantCD { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public DateTime StartDate { get; set; } = default!;
        public DateTime EndDate { get; set; } = default!;
              public Guid LicenseID { get; set; } = default!;
        public bool Active { get; set; } = default!;

    
    }
}

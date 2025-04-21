
namespace eItems.Catalog.Data.Model
{
    public class CostCenter : Entity<Guid>
    {
        public string CostCenterCD { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public bool Active { get; set; } = default!;

        public ICollection<Asset> Assets { get; } = new List<Asset>();
    }
}

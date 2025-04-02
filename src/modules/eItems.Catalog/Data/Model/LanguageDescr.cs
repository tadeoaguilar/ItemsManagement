namespace eItems.Catalog.Data.Model
{
    public class LanguageDescr : Entity<Guid>
    {
        public string LanguageCD { get; set; } = default!;
        public string? Description { get; set; } = default!;

        public ICollection<Asset> Asset { get; } = new List<Asset>();

    }
}

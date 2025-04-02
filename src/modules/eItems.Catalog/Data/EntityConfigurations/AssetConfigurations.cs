

namespace eItems.Catalog.Data.EntityConfigurations

{
    public class AssetConfigurations : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(t => new { t.Id, t.AssetCD, t.SubNumber });
         builder.HasIndex(p => new { p.AssetCD, p.SubNumber });
        }
    }
}

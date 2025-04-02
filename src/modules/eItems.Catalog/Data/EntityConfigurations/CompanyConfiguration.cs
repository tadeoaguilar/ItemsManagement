

namespace eItems.Catalog.Data.EntityConfigurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasIndex(t => t.CompanyCD).IsUnique();
            builder.Property(cb => cb.CompanyCD)
            .HasColumnType("varchar(16)")
            .IsRequired();
        }
    }
}

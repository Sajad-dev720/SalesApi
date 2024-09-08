using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Domain.Entities;

namespace SalesApi.Infrastructure.Persistence.Configurations;

public class SaleLineConfiguration : IEntityTypeConfiguration<SaleLineEntity>
{
    public void Configure(EntityTypeBuilder<SaleLineEntity> builder)
    {
        builder.ToTable("SaleLines", "sales");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title).IsRequired().HasMaxLength(512);

        builder.HasMany(e => e.Products)
            .WithMany(e => e.SaleLines)
            .UsingEntity<SaleLineProductEntity>();

        builder.HasMany(e => e.SalesPeople)
            .WithMany(e => e.SaleLines)
            .UsingEntity<SaleLineSalesPersonEntity>();
    }
}

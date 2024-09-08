using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Domain.Entities;

namespace SalesApi.Infrastructure.Persistence.Configurations;

public class SalesPersonConfiguration : IEntityTypeConfiguration<SalesPersonEntity>
{
    public void Configure(EntityTypeBuilder<SalesPersonEntity> builder)
    {
        builder.ToTable("SalesPeople", "sales");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(128);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(128);
        builder.Property(x => x.FullName).HasComputedColumnSql("(isnull([FirstName],N'')+N' ')+isnull([LastName],N'')");
    }
}

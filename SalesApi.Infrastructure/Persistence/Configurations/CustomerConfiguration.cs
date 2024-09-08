using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Domain.Entities;

namespace SalesApi.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<CustomerEntity>
{
    public void Configure(EntityTypeBuilder<CustomerEntity> builder)
    {
        builder.ToTable("Customers", "sales");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(128);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(128);
        builder.Property(x => x.FullName).HasComputedColumnSql("(isnull([FirstName],N'')+N' ')+isnull([LastName],N'')");
    }
}

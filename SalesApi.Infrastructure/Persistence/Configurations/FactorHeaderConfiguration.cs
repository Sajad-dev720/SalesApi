using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Domain.Entities;

namespace SalesApi.Infrastructure.Persistence.Configurations;

public class FactorHeaderConfiguration : IEntityTypeConfiguration<FactorHeaderEntity>
{
    public void Configure(EntityTypeBuilder<FactorHeaderEntity> builder)
    {
        builder.ToTable("FactorHeaders", "sales");

        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.SaleLine)
            .WithMany(e => e.SaleLineFactorHeaders)
            .HasForeignKey(e => e.SaleLineId)
            .HasConstraintName("SaleLine_FactorHeaders")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Customer)
            .WithMany(e => e.CustomerFactorHeaders)
            .HasForeignKey(e => e.CustomerId)
            .HasConstraintName("Customer_FactorHeaders")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.SalesPerson)
            .WithMany(e => e.SalesPersonFactorHeaders)
            .HasForeignKey(e => e.SalesPersonId)
            .HasConstraintName("SalesPerson_FactorHeaders")
            .OnDelete(DeleteBehavior.Restrict);
    }
}

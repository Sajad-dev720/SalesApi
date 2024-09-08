using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Domain.Entities;

namespace SalesApi.Infrastructure.Persistence.Configurations;

public class DiscountConfiguration : IEntityTypeConfiguration<DiscountEntity>
{
    public void Configure(EntityTypeBuilder<DiscountEntity> builder)
    {
        builder.ToTable("Discounts", "sales");

        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.FactorHeader)
            .WithMany(e => e.FactorHeaderDiscounts)
            .HasForeignKey(e => e.FactorHeaderId)
            .HasConstraintName("FactorHeader_Discounts")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.FactorDetails)
            .WithMany(e => e.FactorDetailsDiscounts)
            .HasForeignKey(e => e.FactorDetailsId)
            .HasConstraintName("FactorDetails_Discounts")
            .OnDelete(DeleteBehavior.Restrict);
    }
}

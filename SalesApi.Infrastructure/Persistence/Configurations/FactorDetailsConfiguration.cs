using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Domain.Entities;

namespace SalesApi.Infrastructure.Persistence.Configurations;

public class FactorDetailsConfiguration : IEntityTypeConfiguration<FactorDetailsEntity>
{
    public void Configure(EntityTypeBuilder<FactorDetailsEntity> builder)
    {
        builder.ToTable("FactorDetails", "sales");

        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Product)
            .WithMany(e => e.ProductFactorDetails)
            .HasForeignKey(e => e.ProductId)
            .HasConstraintName("Product_FactorDetails")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.FactorHeader)
            .WithMany(e => e.FactorDetails)
            .HasForeignKey(e => e.FactorHeaderId)
            .HasConstraintName("FactorHeader_Details")
            .OnDelete(DeleteBehavior.Restrict);
    }
}

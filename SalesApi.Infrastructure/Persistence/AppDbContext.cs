using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SalesApi.Application.Interfaces.Common;
using SalesApi.Domain.Entities;
using SalesApi.Infrastructure.Persistence.Configurations;

namespace SalesApi.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<SaleLineEntity> SaleLines { get; set; }

    public DbSet<ProductEntity> Products { get; set; }

    public DbSet<SaleLineProductEntity> SaleLinesProducts { get; set; }

    public DbSet<SalesPersonEntity> SalesPeople { get; set; }

    public DbSet<SaleLineSalesPersonEntity> SaleLinesSalesPeople { get; set; }

    public DbSet<CustomerEntity> Customers { get; set; }

    public DbSet<FactorHeaderEntity> FactorHeaders { get; set; }

    public DbSet<FactorDetailsEntity> FactorDetails { get; set; }

    public DbSet<DiscountEntity> Discounts { get; set; }

    public DatabaseFacade DatabaseFacade => Database;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var now = DateTime.Now;
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = now;
                    entry.Entity.ModifiedAt = now;
                    break;

                case EntityState.Modified:
                    entry.Property(x => x.CreatedAt).IsModified = false;
                    entry.Entity.ModifiedAt = now;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(SaleLineConfiguration).Assembly);
        base.OnModelCreating(builder);
    }
}

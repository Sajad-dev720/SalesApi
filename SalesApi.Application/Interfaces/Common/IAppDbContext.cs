using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SalesApi.Domain.Entities;

namespace SalesApi.Application.Interfaces.Common;
public interface IAppDbContext
{
    DbSet<SaleLineEntity> SaleLines { get; set; }

    DbSet<ProductEntity> Products { get; set; }

    DbSet<SaleLineProductEntity> SaleLinesProducts { get; set; }

    DbSet<SalesPersonEntity> SalesPeople { get; set; }

    DbSet<SaleLineSalesPersonEntity> SaleLinesSalesPeople { get; set;}

    DbSet<CustomerEntity> Customers { get; set; }

    DbSet<FactorHeaderEntity> FactorHeaders { get; set; }

    DbSet<FactorDetailsEntity> FactorDetails { get; set; }

    DbSet<DiscountEntity> Discounts { get; set; }

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    DatabaseFacade DatabaseFacade { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

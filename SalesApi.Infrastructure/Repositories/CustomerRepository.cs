using Microsoft.EntityFrameworkCore;
using SalesApi.Application.Interfaces.Common;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.Domain.Entities;
using SalesApi.Domain.Enums;

namespace SalesApi.Infrastructure.Repositories;

public class CustomerRepository(IAppDbContext context) : ICustomerRepository
{
    private readonly IAppDbContext _context = context;

    public async Task AddAsync(CustomerEntity entity, CancellationToken cancellationToken = default)
    {
        await _context
            .Customers
            .AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> CustomerExeecedAMilion(Guid id, CancellationToken cancellationToken = default)
    {
        var query = _context.FactorHeaders
            .Where(w => w.CustomerId == id && w.Status == FactorStatus.Final)
            .Select(s => new
            {
                DetailsSum = s.FactorDetails.Sum(s => s.Price),
                DetailsDiscounts = s.FactorDetails.SelectMany(s => s.FactorDetailsDiscounts),
                HeaderDiscountSum = s.FactorHeaderDiscounts.Sum(s => s.DiscountAmount),
            });

        var result = await query.ToListAsync(cancellationToken);

        var detailsPrices = result.Sum(s => s.DetailsSum);
        var headerDiscountSum = result.Sum(s => s.HeaderDiscountSum);
        var detailsDiscountSum = result.Sum(s => s.DetailsDiscounts.Sum(x => x.DiscountAmount));

        return detailsPrices - (headerDiscountSum + detailsDiscountSum) > 1000000;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context
            .Customers
            .Where(w => w.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<bool> Exists(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Customers
            .Where(w => w.Id == id)
            .AnyAsync(cancellationToken);
    }

    public async Task<CustomerEntity?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Customers
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<CustomerEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context
            .Customers
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(CustomerEntity entity, CancellationToken cancellationToken = default)
    {
        _context
            .Customers
            .Update(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }


}
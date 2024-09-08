using Microsoft.EntityFrameworkCore;
using SalesApi.Application.Interfaces.Common;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.Domain.Entities;

namespace SalesApi.Infrastructure.Repositories;

public class ProductRepository(IAppDbContext context) : IProductRepository
{
    private readonly IAppDbContext _context = context;

    public async Task AddAsync(ProductEntity entity, CancellationToken cancellationToken = default)
    {
        await _context
            .Products
            .AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context
            .Products
            .Where(w => w.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<ProductEntity?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Products
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context
            .Products
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProductEntity entity, CancellationToken cancellationToken = default)
    {
        _context
            .Products
            .Update(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> Exists(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Products
            .Where(w => w.Id == id)
            .AnyAsync(cancellationToken);
    }
}

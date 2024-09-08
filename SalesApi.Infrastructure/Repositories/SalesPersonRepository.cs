using Microsoft.EntityFrameworkCore;
using SalesApi.Application.Interfaces.Common;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.Domain.Entities;

namespace SalesApi.Infrastructure.Repositories;

public class SalesPersonRepository(IAppDbContext context) : ISalesPersonRepostory
{
    private readonly IAppDbContext _context = context;

    public async Task AddAsync(SalesPersonEntity entity, CancellationToken cancellationToken = default)
    {
        await _context
            .SalesPeople
            .AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context
            .SalesPeople
            .Where(w => w.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<SalesPersonEntity?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .SalesPeople
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<SalesPersonEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context
            .SalesPeople
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(SalesPersonEntity entity, CancellationToken cancellationToken = default)
    {
        _context
            .SalesPeople
            .Update(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> Exists(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .SalesPeople
            .Where(w => w.Id == id)
            .AnyAsync(cancellationToken);
    }
}

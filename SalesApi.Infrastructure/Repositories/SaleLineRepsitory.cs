using Microsoft.EntityFrameworkCore;
using SalesApi.Application.Interfaces.Common;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.Domain.Entities;

namespace SalesApi.Infrastructure.Repositories;

public class SaleLineRepsitory(IAppDbContext context) : ISaleLineRepository
{
    private readonly IAppDbContext _context = context;

    public async Task AddAsync(SaleLineEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.SaleLines.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context
            .SaleLines
            .Where(w => w.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<SaleLineEntity?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .SaleLines
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<SaleLineEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context
            .SaleLines
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(SaleLineEntity entity, CancellationToken cancellationToken = default)
    {
        _context
            .SaleLines
            .Update(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> Exists(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .SaleLines
            .Where(w => w.Id == id)
            .AnyAsync(cancellationToken);
    }
}
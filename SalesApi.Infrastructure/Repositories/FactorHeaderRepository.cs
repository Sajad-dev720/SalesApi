using Microsoft.EntityFrameworkCore;
using SalesApi.Application.FactorHeader.Dtos;
using SalesApi.Application.Interfaces.Common;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.Domain.Entities;
using SalesApi.Domain.Enums;

namespace SalesApi.Infrastructure.Repositories;

public class FactorHeaderRepository(IAppDbContext context) : IFactorHeaderRepository
{
    private readonly IAppDbContext _context = context;

    public async Task AddAsync(FactorHeaderEntity entity, CancellationToken cancellationToken = default)
    {
        await _context
            .FactorHeaders
            .AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context
            .FactorHeaders
            .Where(w => w.Id == id)
            .ExecuteUpdateAsync(e =>
                e.SetProperty(p => p.IsDeleted, true)
                .SetProperty(p => p.DeletedAt, DateTime.Now),
                cancellationToken);
    }

    public async Task<FactorHeaderEntity?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .FactorHeaders
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<FactorHeaderEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context
            .FactorHeaders
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(FactorHeaderEntity entity, CancellationToken cancellationToken = default)
    {
        _context
            .FactorHeaders
            .Update(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> Exists(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .FactorHeaders
            .Where(w => w.Id == id)
            .AnyAsync(cancellationToken);
    }

    public async Task<bool> IsDraft(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .FactorHeaders
            .Where(w =>
                w.Id == id &&
                w.Status == FactorStatus.Draft)
            .AnyAsync(cancellationToken);
    }

    public async Task FinalizeFactorHeader(Guid id, CancellationToken cancellationToken = default)
    {
        await _context
            .FactorHeaders
            .Where(w => w.Id == id)
            .ExecuteUpdateAsync(e =>
                e.SetProperty(p => p.Status, FactorStatus.Final)
                .SetProperty(p => p.ModifiedAt, DateTime.Now),
                cancellationToken);
    }

    public async Task<bool> IsSalesPersonInLine(Guid salesPersonId, Guid lineId, CancellationToken cancellationToken = default)
    {
        return await _context
            .SaleLinesSalesPeople
            .Where(w =>
                w.SalesPersonId == salesPersonId &&
                w.SaleLineId == lineId)
            .AnyAsync(cancellationToken);
    }

    public async Task<bool> HasDependencies(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .FactorHeaders
            .Where(w =>
                w.Id == id &&
                (w.FactorDetails != null ||
                w.FactorHeaderDiscounts != null))
            .AnyAsync(cancellationToken);
    }

    public async Task<FactorHeaderDetailsModel?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .FactorHeaders
            .Where(w => w.Id == id)
            .Select(s => new FactorHeaderDetailsModel
            {
                Id = s.Id,
                CustomerFullName = s.Customer!.FullName,
                SaleLineTitle = s.SaleLine!.Title,
                SalesPersonFullName = s.SalesPerson!.FullName,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<FactorHeaderDetailsModel>> GetAll(CancellationToken cancellationToken = default)
    {
        var query = _context
            .FactorHeaders
            .Select(s => new FactorHeaderDetailsModel
            {
                Id = s.Id,
                CustomerFullName = s.Customer!.FullName,
                SaleLineTitle = s.SaleLine!.Title,
                SalesPersonFullName = s.SalesPerson!.FullName,
            });

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<bool> HasDetails(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .FactorHeaders
            .Where(w =>
                w.Id == id &&
                w.FactorDetails != null)
            .AnyAsync(cancellationToken);
    }
}
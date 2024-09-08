using Microsoft.EntityFrameworkCore;
using SalesApi.Application.FactorDetails.Dtos;
using SalesApi.Application.FactorHeader.Dtos;
using SalesApi.Application.Interfaces.Common;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.Domain.Entities;
using SalesApi.Domain.Enums;

namespace SalesApi.Infrastructure.Repositories;

public class FactorDetailsRepository(IAppDbContext context) : IFactorDetailsRepository
{
    private readonly IAppDbContext _context = context;

    public async Task AddAsync(FactorDetailsEntity entity, CancellationToken cancellationToken = default)
    {
        await _context
            .FactorDetails
            .AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context
            .FactorDetails
            .Where(w => w.Id == id)
            .ExecuteUpdateAsync(e =>
                e.SetProperty(p => p.IsDeleted, true)
                .SetProperty(p => p.DeletedAt, DateTime.Now),
                cancellationToken);
    }

    public async Task<bool> Exists(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .FactorDetails
            .Where(w => w.Id == id)
            .AnyAsync(cancellationToken);
    }

    public async Task<FactorDetailsEntity?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .FactorDetails
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<FactorDetailsEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context
            .FactorDetails
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(FactorDetailsEntity entity, CancellationToken cancellationToken = default)
    {
        _context
            .FactorDetails
            .Update(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsDuplicateProduct(Guid factorHeaderId, Guid productId, CancellationToken cancellationToken = default)
    {
        var count = await _context
            .FactorDetails
            .Where(w =>
                w.FactorHeaderId == factorHeaderId &&
                w.ProductId == productId)
            .CountAsync(cancellationToken);

        return count >= 1;
    }

    public async Task<bool> IsProductInLine(Guid factorHeaderId, Guid productId, CancellationToken cancellationToken)
    {
        var lineId = await _context
            .FactorHeaders
            .Where(w =>
                w.Id == factorHeaderId)
            .Select(s => s.SaleLineId)
            .FirstOrDefaultAsync(cancellationToken);

        return await _context
            .SaleLinesProducts
            .Where(w => w.SaleLineId == lineId && w.ProductId == productId)
            .AnyAsync(cancellationToken);
    }

    public async Task<bool> IsHeaderFinal(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .FactorDetails
            .Where(w =>
                w.Id == id &&
                w.FactorHeader!.Status == FactorStatus.Final)
            .AnyAsync(cancellationToken);
    }

    public async Task<FactorDetailsDetailsModel?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .FactorDetails
            .Where(w => w.Id == id)
            .Select(s => new FactorDetailsDetailsModel
            {
                Id = s.Id,
                ProductTitle = s.Product!.Title,
                Count = s.Count,
                Price = s.Price,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<FactorDetailsDetailsModel>> GetAll(CancellationToken cancellationToken = default)
    {
        var query = _context
            .FactorDetails
            .Select(s => new FactorDetailsDetailsModel
            {
                Id = s.Id,
                ProductTitle = s.Product!.Title,
                Count = s.Count,
                Price = s.Price,
            });

        return await query.ToListAsync(cancellationToken);
    }
}
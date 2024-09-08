using Microsoft.EntityFrameworkCore;
using SalesApi.Application.Discount.Dtos;
using SalesApi.Application.Interfaces.Common;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.Domain.Entities;
using SalesApi.Domain.Enums;

namespace SalesApi.Infrastructure.Repositories;

public class DiscountRepository(IAppDbContext context) : IDiscountRepository
{
    private readonly IAppDbContext _context = context;

    public async Task AddAsync(DiscountEntity entity, CancellationToken cancellationToken = default)
    {
        await _context
            .Discounts
            .AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context
            .Discounts
            .Where(w => w.Id == id)
            .ExecuteUpdateAsync(e =>
                e.SetProperty(p => p.IsDeleted, true)
                .SetProperty(p => p.DeletedAt, DateTime.Now),
                cancellationToken);
    }

    public async Task<DiscountEntity?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Discounts
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<DiscountEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context
            .Discounts
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(DiscountEntity entity, CancellationToken cancellationToken = default)
    {
        _context
           .Discounts
           .Update(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> Exists(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Discounts
            .Where(w => w.Id == id)
            .AnyAsync(cancellationToken);
    }

    public async Task<bool> HasExceededDoc(Guid factorHeaderId, long discountAmount, CancellationToken cancellationToken)
    {
        var data = await _context
            .FactorHeaders
            .Where(w => w.Id == factorHeaderId)
            .Select(s => new
            {
                s.Id,
                FactorSum = s.FactorDetails.Sum(s => s.Price),
                DiscountSum = s.FactorHeaderDiscounts.Sum(s =>s.DiscountAmount),
            })
            .FirstOrDefaultAsync(cancellationToken);

        return data!.FactorSum < data.DiscountSum + discountAmount;
    }

    public async Task<bool> HasExceededRow(Guid factorDetaislId, long discountAmount, CancellationToken cancellationToken)
    {
        var data = await _context
            .FactorDetails
            .Where(w => w.Id == factorDetaislId)
            .Select(s => new
            {
                s.Id,
                FactorSum = s.Price,
                DiscountSum = s.FactorDetailsDiscounts.Sum(s => s.DiscountAmount),
            })
            .FirstOrDefaultAsync(cancellationToken);

        return data!.FactorSum < data.DiscountSum + discountAmount;
    }

    public async Task<bool> HasExceededDocEdit(Guid factorHeaderId, Guid discountId, long discountAmount, CancellationToken cancellationToken)
    {
        var data = await _context
            .FactorHeaders
            .Where(w =>
                w.Id == factorHeaderId)
            .Select(s => new
            {
                s.Id,
                FactorSum = s.FactorDetails.Sum(s => s.Price),
                DiscountSum = s.FactorHeaderDiscounts.Where(w => w.Id != discountId).Sum(s => s.DiscountAmount),
            })
            .FirstOrDefaultAsync(cancellationToken);

        return data!.FactorSum < data.DiscountSum + discountAmount;
    }

    public async Task<bool> HasExceededRowEdit(Guid factorDetaislId, Guid discountId, long discountAmount, CancellationToken cancellationToken)
    {
        var data = await _context
            .FactorDetails
            .Where(w => w.Id == factorDetaislId)
            .Select(s => new
            {
                s.Id,
                FactorSum = s.Price,
                DiscountSum = s.FactorDetailsDiscounts.Where(w => w.Id != discountId).Sum(s => s.DiscountAmount),
            })
            .FirstOrDefaultAsync(cancellationToken);

        return data!.FactorSum < data.DiscountSum + discountAmount;
    }

    public async Task<DiscountDetailsModel?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Discounts
            .Where(w => w.Id == id)
            .Select(s => new DiscountDetailsModel
            {
                Id = s.Id,
                DiscountAmount = s.DiscountAmount,
                FactorDetailsId = s.FactorDetailsId,
                FactorHeaderId = s.FactorHeaderId,
                Type = s.Type,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<DiscountDetailsModel>> GetAll(CancellationToken cancellationToken = default)
    {
        var query = _context
            .Discounts
            .Select(s => new DiscountDetailsModel
            {
                Id = s.Id,
                DiscountAmount = s.DiscountAmount,
                FactorDetailsId = s.FactorDetailsId,
                FactorHeaderId = s.FactorHeaderId,
                Type = s.Type,
            });

        return await query.ToListAsync(cancellationToken);
    }
}

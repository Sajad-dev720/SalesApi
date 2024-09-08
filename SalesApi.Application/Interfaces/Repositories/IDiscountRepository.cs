using SalesApi.Application.Discount.Dtos;
using SalesApi.Domain.Entities;
using SalesApi.Domain.Enums;

namespace SalesApi.Application.Interfaces.Repositories;
public interface IDiscountRepository : IBaseRepository<DiscountEntity>
{
    Task<bool> HasExceededDoc(Guid factorHeaderId, long discountAmount, CancellationToken cancellationToken);

    Task<bool> HasExceededRow(Guid factorDetaislId, long discountAmount, CancellationToken cancellationToken);

    Task<bool> HasExceededDocEdit(Guid factorHeaderId, Guid discountId, long discountAmount, CancellationToken cancellationToken);

    Task<bool> HasExceededRowEdit(Guid factorDetaislId, Guid discountId, long discountAmount, CancellationToken cancellationToken);

    Task<DiscountDetailsModel?> GetById(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<DiscountDetailsModel>> GetAll(CancellationToken cancellationToken = default);
}

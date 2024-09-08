using SalesApi.Application.FactorDetails.Dtos;
using SalesApi.Domain.Entities;

namespace SalesApi.Application.Interfaces.Repositories;
public interface IFactorDetailsRepository : IBaseRepository<FactorDetailsEntity>
{
    Task<bool> IsProductInLine(Guid factorHeaderId, Guid productId, CancellationToken cancellationToken);

    Task<bool> IsDuplicateProduct(Guid factorHeaderId, Guid productId, CancellationToken cancellationToken = default);

    Task<bool> IsHeaderFinal(Guid id, CancellationToken cancellationToken = default);

    Task<FactorDetailsDetailsModel?> GetById(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<FactorDetailsDetailsModel>> GetAll(CancellationToken cancellationToken = default);
}

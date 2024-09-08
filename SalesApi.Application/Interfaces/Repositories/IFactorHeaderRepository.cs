using SalesApi.Application.FactorHeader.Dtos;
using SalesApi.Domain.Entities;

namespace SalesApi.Application.Interfaces.Repositories;
public interface IFactorHeaderRepository : IBaseRepository<FactorHeaderEntity>
{
    Task<FactorHeaderDetailsModel?> GetById(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<FactorHeaderDetailsModel>> GetAll(CancellationToken cancellationToken = default);

    Task<bool> IsDraft(Guid id, CancellationToken cancellationToken = default);

    Task FinalizeFactorHeader(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsSalesPersonInLine(Guid salesPersonId, Guid lineId, CancellationToken cancellationToken = default);

    Task<bool> HasDependencies(Guid id, CancellationToken cancellationToken = default);

    Task<bool> HasDetails(Guid id, CancellationToken cancellationToken = default);

}

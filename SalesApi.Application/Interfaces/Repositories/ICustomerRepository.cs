using SalesApi.Domain.Entities;

namespace SalesApi.Application.Interfaces.Repositories;
public interface ICustomerRepository : IBaseRepository<CustomerEntity>
{
    Task<bool> CustomerExeecedAMilion(Guid id, CancellationToken cancellationToken = default);
}

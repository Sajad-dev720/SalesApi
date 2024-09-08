using MediatR;
using SalesApi.Application.FactorHeader.Dtos;
using SalesApi.Application.Interfaces.Repositories;

namespace SalesApi.Application.FactorHeader.Queries;

public static class GetFactorHeaders
{
    public record GetAllQuery : IRequest<IEnumerable<FactorHeaderDetailsModel>>;

    internal class Handler(IFactorHeaderRepository factorHeaderRepository) : IRequestHandler<GetAllQuery, IEnumerable<FactorHeaderDetailsModel>>
    {
        private readonly IFactorHeaderRepository _factorHeaderRepository = factorHeaderRepository;

        public async Task<IEnumerable<FactorHeaderDetailsModel>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _factorHeaderRepository.GetAll(cancellationToken);
        }
    }
}

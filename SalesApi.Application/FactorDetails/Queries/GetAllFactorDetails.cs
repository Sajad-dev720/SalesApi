using MediatR;
using SalesApi.Application.FactorDetails.Dtos;
using SalesApi.Application.Interfaces.Repositories;

namespace SalesApi.Application.FactorDetails.Queries;

public static class GetAllFactorDetails
{
    public record GetAllQuery : IRequest<IEnumerable<FactorDetailsDetailsModel>>;

    internal class Handler(IFactorDetailsRepository factorDetailsRepository) : IRequestHandler<GetAllQuery, IEnumerable<FactorDetailsDetailsModel>>
    {
        private readonly IFactorDetailsRepository _factorDetailsRepository = factorDetailsRepository;

        public async Task<IEnumerable<FactorDetailsDetailsModel>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _factorDetailsRepository.GetAll(cancellationToken);
        }
    }
}

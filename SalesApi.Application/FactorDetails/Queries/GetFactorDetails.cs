using MediatR;
using SalesApi.Application.FactorDetails.Dtos;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.CrossCutting.Exceptions;

namespace SalesApi.Application.FactorDetails.Queries;

public static class GetFactorDetails
{
    public record GetQuery(Guid Id) : IRequest<FactorDetailsDetailsModel>;

    internal class Handler(IFactorDetailsRepository factorDetailsRepository) : IRequestHandler<GetQuery, FactorDetailsDetailsModel>
    {
        private readonly IFactorDetailsRepository _factorDetailsRepository = factorDetailsRepository;

        public async Task<FactorDetailsDetailsModel> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            return await _factorDetailsRepository
                .GetById(
                id: request.Id,
                cancellationToken: cancellationToken)
                ?? throw new SalesApiNotFoundException("Factor header not found");
        }
    }
}

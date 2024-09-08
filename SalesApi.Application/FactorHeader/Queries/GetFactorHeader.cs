using MediatR;
using SalesApi.Application.FactorHeader.Dtos;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.CrossCutting.Exceptions;

namespace SalesApi.Application.FactorHeader.Queries;

public static class GetFactorHeader
{
    public record GetQuery(Guid Id) : IRequest<FactorHeaderDetailsModel>;

    internal class Handler(IFactorHeaderRepository factorHeaderRepository) : IRequestHandler<GetQuery, FactorHeaderDetailsModel>
    {
        private readonly IFactorHeaderRepository _factorHeaderRepository = factorHeaderRepository;

        public async Task<FactorHeaderDetailsModel> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            return await _factorHeaderRepository
                .GetById(
                id: request.Id,
                cancellationToken: cancellationToken)
                ?? throw new SalesApiNotFoundException("Factor header not found");
        }
    }
}
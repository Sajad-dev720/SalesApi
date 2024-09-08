using MediatR;
using SalesApi.Application.Factor.Dtos;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.CrossCutting.Exceptions;
using SalesApi.Domain.Entities;

namespace SalesApi.Application.FactorHeader.Commands;

public static class AddFactorHeader
{
    public record AddCommand(FactorHeaderAddModel Model) : IRequest;

    internal class Handler(IFactorHeaderRepository factorHeaderRepository) : IRequestHandler<AddCommand>
    {
        private readonly IFactorHeaderRepository _factorHeaderRepository = factorHeaderRepository;

        public async Task Handle(AddCommand request, CancellationToken cancellationToken)
        {
            var isSalePersonInLine = await _factorHeaderRepository.IsSalesPersonInLine(
                salesPersonId: request.Model.SalePersonId,
                lineId: request.Model.SaleLineId,
                cancellationToken: cancellationToken);

            if (!isSalePersonInLine)
            {
                throw new SalesApiAppException("Sales person must be registered in sale line");
            }

            var entity = new FactorHeaderEntity(
                saleLineId: request.Model.SaleLineId,
                customerId: request.Model.CustomerId,
                salesPersonId: request.Model.SalePersonId);

            await _factorHeaderRepository.AddAsync(entity, cancellationToken);
        }
    }
}
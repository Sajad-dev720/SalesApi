using MediatR;
using SalesApi.Application.FactorHeader.Dtos;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.CrossCutting.Exceptions;
using SalesApi.Domain.Enums;

namespace SalesApi.Application.FactorHeader.Commands;

public static class EditFactorHeader
{
    public record EditCommand(FactorHeaderEditModel Model) : IRequest;

    internal class Handler(IFactorHeaderRepository factorHeaderRepository) : IRequestHandler<EditCommand>
    {
        private readonly IFactorHeaderRepository _factorHeaderRepository = factorHeaderRepository;

        public async Task Handle(EditCommand request, CancellationToken cancellationToken)
        {
            var entity = await _factorHeaderRepository
                .FindAsync(
                id: request.Model.Id,
                cancellationToken: cancellationToken)
                ?? throw new SalesApiNotFoundException("Factor not found");

            if (entity.Status == FactorStatus.Final)
            {
                throw new SalesApiAppException("Cannot edit a finalized factor");
            }

            var hasDetails = await _factorHeaderRepository
                .HasDetails(
                id: request.Model.Id,
                cancellationToken: cancellationToken);

            if (entity.SaleLineId != request.Model.SaleLineId && hasDetails)
            {
                throw new SalesApiValidationException(nameof(request.Model.SaleLineId), ["Cannot edit sale line when factor has details"]);
            }

            entity.SaleLineId = request.Model.SaleLineId;
            entity.SalesPersonId = request.Model.SalePersonId;
            entity.CustomerId = request.Model.CustomerId;

            await _factorHeaderRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}

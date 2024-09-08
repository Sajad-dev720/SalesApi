using FluentValidation;
using MediatR;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.CrossCutting.Exceptions;
using SalesApi.Domain.Enums;

namespace SalesApi.Application.Factor.Commands;

public static class FinalizeFactor
{
    public record FinalizeCommand(Guid Id) : IRequest;

    internal class Handler(IFactorHeaderRepository factorHeaderRepository, ICustomerRepository customerRepository) 
        : IRequestHandler<FinalizeCommand>
    {
        private readonly IFactorHeaderRepository _factorHeaderRepository = factorHeaderRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task Handle(FinalizeCommand request, CancellationToken cancellationToken)
        {
            var factorHeader = await _factorHeaderRepository.FindAsync(request.Id, cancellationToken)
                ?? throw new SalesApiNotFoundException("Factor not found");

            if (factorHeader.Status != FactorStatus.Draft)
            {
                throw new SalesApiAppException("Factor is not a draft");
            }

            var exceededAMilion = await _customerRepository.CustomerExeecedAMilion(
                id: factorHeader.CustomerId,
                cancellationToken: cancellationToken);

            if (exceededAMilion)
            {
                throw new SalesApiAppException("Customer's final factors exceeded a milion");
            }

            await _factorHeaderRepository.FinalizeFactorHeader(request.Id, cancellationToken);
        }
    }
}

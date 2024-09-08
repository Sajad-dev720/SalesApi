using MediatR;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.CrossCutting.Exceptions;

namespace SalesApi.Application.FactorHeader.Commands;

public static class DeleteFactorHeader
{
    public record DeleteCommand(Guid Id) : IRequest;

    internal class Handler(IFactorHeaderRepository factorHeaderRepository) : IRequestHandler<DeleteCommand>
    {
        private readonly IFactorHeaderRepository _factorHeaderRepository = factorHeaderRepository;

        public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var exists = await _factorHeaderRepository.Exists(
                id: request.Id,
                cancellationToken: cancellationToken);

            if (!exists)
            {
                throw new SalesApiNotFoundException("Factor header not found");
            }

            var hasDependency = await _factorHeaderRepository
                .HasDependencies(
                id: request.Id,
                cancellationToken: cancellationToken);

            if (hasDependency)
            {
                throw new SalesApiAppException("There is at least 1 factor details and/or discount submitted that's related to this factor");
            }

            await _factorHeaderRepository.DeleteAsync(
                id: request.Id,
                cancellationToken: cancellationToken);
        }
    }
}

using MediatR;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.CrossCutting.Exceptions;

namespace SalesApi.Application.FactorDetails.Commands;

public static class DeleteFactorDetails
{
    public record DeleteCommand(Guid Id) : IRequest;

    internal class Handler(IFactorDetailsRepository factorDetailsRepository) : IRequestHandler<DeleteCommand>
    {
        private readonly IFactorDetailsRepository _factorDetailsRepository = factorDetailsRepository;

        public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var exists = await _factorDetailsRepository.Exists(
                id: request.Id,
                cancellationToken: cancellationToken);

            if (!exists)
            {
                throw new SalesApiNotFoundException("Factor details not found");
            }

            await _factorDetailsRepository.DeleteAsync(
                id: request.Id,
                cancellationToken: cancellationToken);
        }
    }
}

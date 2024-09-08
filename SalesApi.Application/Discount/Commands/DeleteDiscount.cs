using MediatR;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.CrossCutting.Exceptions;

namespace SalesApi.Application.Discount.Commands;

public static class DeleteDiscount
{
    public record DeleteCommand(Guid Id) : IRequest;

    internal class Handler(IDiscountRepository discountRepository) : IRequestHandler<DeleteCommand>
    {
        private readonly IDiscountRepository _discountRepository = discountRepository;

        public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var exists = await _discountRepository.Exists(
                id: request.Id,
                cancellationToken: cancellationToken);

            if (!exists)
            {
                throw new SalesApiNotFoundException("Discount not found");
            }

            await _discountRepository.DeleteAsync(
               id: request.Id,
               cancellationToken: cancellationToken);
        }
    }
}

using MediatR;
using SalesApi.Application.Discount.Dtos;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.CrossCutting.Exceptions;

namespace SalesApi.Application.Discount.Queries;

public static class GetDiscount
{
    public record GetQuery(Guid Id) : IRequest<DiscountDetailsModel>;

    internal class Handler(IDiscountRepository discountRepository) : IRequestHandler<GetQuery, DiscountDetailsModel>
    {
        private readonly IDiscountRepository _discountRepository = discountRepository;

        public async Task<DiscountDetailsModel> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            return await _discountRepository
                .GetById(
                id: request.Id,
                cancellationToken: cancellationToken)
                ?? throw new SalesApiNotFoundException("Discount not found");
        }
    }
}

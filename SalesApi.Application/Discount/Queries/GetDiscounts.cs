using MediatR;
using SalesApi.Application.Discount.Dtos;
using SalesApi.Application.Interfaces.Repositories;

namespace SalesApi.Application.Discount.Queries;

public static class GetDiscounts
{
    public record GetAllQuery : IRequest<IEnumerable<DiscountDetailsModel>>;

    internal class Handler(IDiscountRepository discountRepository) : IRequestHandler<GetAllQuery, IEnumerable<DiscountDetailsModel>>
    {
        private readonly IDiscountRepository _discountRepository = discountRepository;

        public async Task<IEnumerable<DiscountDetailsModel>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _discountRepository.GetAll(cancellationToken);
        }
    }
}

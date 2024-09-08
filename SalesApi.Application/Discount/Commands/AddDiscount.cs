using FluentValidation;
using MediatR;
using SalesApi.Application.Discount.Dtos;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.CrossCutting.Exceptions;
using SalesApi.Domain.Entities;
using SalesApi.Domain.Enums;

namespace SalesApi.Application.Discount.Commands;

public static class AddDiscount
{
    public record AddCommand(DiscountAddModel Model) : IRequest;

	public class AddValidator : AbstractValidator<AddCommand>
	{
        public AddValidator()
        {
            RuleFor(x => x.Model.DiscountAmount).NotEmpty();

            RuleFor(x => x.Model.FactorDetailsId).NotNull()
                .When(w => w.Model.Type == DiscountType.Row);

            RuleFor(x => x.Model.FactorDetailsId).Null()
                .When(w => w.Model.Type == DiscountType.Doc);
        }
    }

    internal class Handler(IDiscountRepository discountRepository, IFactorHeaderRepository factorHeaderRepository, IFactorDetailsRepository factorDetailsRepository)
        : IRequestHandler<AddCommand>
    {
        private readonly IDiscountRepository _discountRepository = discountRepository;
        private readonly IFactorHeaderRepository _factorHeaderRepository = factorHeaderRepository;
        private readonly IFactorDetailsRepository _factorDetailsRepository = factorDetailsRepository;

        public async Task Handle(AddCommand request, CancellationToken cancellationToken)
        {
            if (request.Model.Type == DiscountType.Row)
            {
                var exists = await _factorDetailsRepository.Exists(
                    id: (Guid)request.Model.FactorDetailsId!,
                    cancellationToken: cancellationToken);

                if (!exists)
                {
                    throw new SalesApiNotFoundException("Factor details not found");
                }

                var hasExceeded = await _discountRepository.HasExceededRow(
                    factorDetaislId: (Guid)request.Model.FactorDetailsId!,
                    discountAmount: request.Model.DiscountAmount,
                    cancellationToken: cancellationToken);

                if (hasExceeded)
                {
                    throw new SalesApiAppException("Discount amount has exceeded the price of factor");
                }

                var entity = new DiscountEntity(
                    factorHeaderId: request.Model.FactorHeaderId,
                    type: request.Model.Type,
                    factorDetailsId: request.Model.FactorDetailsId);

                await _discountRepository.AddAsync(entity, cancellationToken);
            }
            else
            {
                var exists = await _factorHeaderRepository.Exists(
                    id: request.Model.FactorHeaderId,
                    cancellationToken: cancellationToken);

                if (!exists)
                {
                    throw new SalesApiNotFoundException("Factor header not found");
                }

                var hasExceeded = await _discountRepository.HasExceededDoc(
                    factorHeaderId: request.Model.FactorHeaderId,
                    discountAmount: request.Model.DiscountAmount,
                    cancellationToken: cancellationToken);

                if (hasExceeded)
                {
                    throw new SalesApiAppException("Discount amount has exceeded the price of factor");
                }

                var entity = new DiscountEntity(
                    factorHeaderId: request.Model.FactorHeaderId,
                    type: request.Model.Type);

                await _discountRepository.AddAsync(entity, cancellationToken);
            }
        }
    }
}

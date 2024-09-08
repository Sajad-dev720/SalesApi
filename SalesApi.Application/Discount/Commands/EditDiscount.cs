using FluentValidation;
using MediatR;
using SalesApi.Application.Discount.Dtos;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.CrossCutting.Exceptions;
using SalesApi.Domain.Entities;
using SalesApi.Domain.Enums;

namespace SalesApi.Application.Discount.Commands;

public static class EditDiscount
{
    public record EditCommand(DiscountEditModel Model) : IRequest;

    public class EditValidator : AbstractValidator<EditCommand>
    {
        public EditValidator()
        {
            RuleFor(x => x.Model.DiscountAmount).NotEmpty();
        }
    }

    internal class Handler(IDiscountRepository discountRepository, IFactorHeaderRepository factorHeaderRepository, IFactorDetailsRepository factorDetailsRepository)
        : IRequestHandler<EditCommand>
    {
        private readonly IDiscountRepository _discountRepository = discountRepository;
        private readonly IFactorHeaderRepository _factorHeaderRepository = factorHeaderRepository;
        private readonly IFactorDetailsRepository _factorDetailsRepository = factorDetailsRepository;

        public async Task Handle(EditCommand request, CancellationToken cancellationToken)
        {
            var entity = await _discountRepository
                .FindAsync(
                id: request.Model.Id,
                cancellationToken: cancellationToken)
                ?? throw new SalesApiNotFoundException("Discount not found");

            if (entity.Type == DiscountType.Row)
            {
                var exists = await _factorDetailsRepository.Exists(
                    id: (Guid)entity.FactorDetailsId!,
                    cancellationToken: cancellationToken);

                if (!exists)
                {
                    throw new SalesApiNotFoundException("Factor details not found");
                }

                var hasExceeded = await _discountRepository.HasExceededRowEdit(
                    factorDetaislId: (Guid)entity.FactorDetailsId!,
                    discountId: entity.Id,
                    discountAmount: entity.DiscountAmount,
                    cancellationToken: cancellationToken);

                if (hasExceeded)
                {
                    throw new SalesApiAppException("Discount amount has exceeded the price of factor");
                }
            }
            else
            {
                var exists = await _factorHeaderRepository.Exists(
                    id: entity.FactorHeaderId,
                    cancellationToken: cancellationToken);

                if (!exists)
                {
                    throw new SalesApiNotFoundException("Factor header not found");
                }

                var hasExceeded = await _discountRepository.HasExceededDocEdit(
                    factorHeaderId: entity.FactorHeaderId,
                    discountId: entity.Id,
                    discountAmount: entity.DiscountAmount,
                    cancellationToken: cancellationToken);

                if (hasExceeded)
                {
                    throw new SalesApiAppException("Discount amount has exceeded the price of factor");
                }
            }

            entity.DiscountAmount = request.Model.DiscountAmount;

            await _discountRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}
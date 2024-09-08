using FluentValidation;
using MediatR;
using SalesApi.Application.FactorDetails.Dtos;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.CrossCutting.Exceptions;
using SalesApi.Domain.Entities;

namespace SalesApi.Application.FactorDetails.Commands;

public static class AddFactorDetails
{
    public record AddCommand(FactorDetailsAddModel Model) : IRequest;

	public class AddValidator : AbstractValidator<AddCommand>
	{
        public AddValidator()
        {
            RuleFor(x => x.Model.ProductId).NotEmpty();
            RuleFor(x => x.Model.Count).NotEmpty();
            RuleFor(x => x.Model.Price).NotEmpty();
        }
    }

    internal class Handler(IFactorDetailsRepository factorDetailsRepository, IFactorHeaderRepository factorHeaderRepository) 
        : IRequestHandler<AddCommand>
    {
        private readonly IFactorDetailsRepository _factorDetailsRepository = factorDetailsRepository;
        private readonly IFactorHeaderRepository _factorHeaderRepository = factorHeaderRepository;

        public async Task Handle(AddCommand request, CancellationToken cancellationToken)
        {
            var factorHeaderExists = await _factorHeaderRepository.Exists(
                id: request.Model.FactorHeaderId,
                cancellationToken: cancellationToken);

            if (!factorHeaderExists)
            {
                throw new SalesApiValidationException(nameof(request.Model.FactorHeaderId), ["Factor header not found"]);
            }

            var isProductInLine = await _factorDetailsRepository.IsProductInLine(
                factorHeaderId: request.Model.FactorHeaderId,
                productId: request.Model.ProductId,
                cancellationToken: cancellationToken);

            if (!isProductInLine)
            {
                throw new SalesApiValidationException(nameof(request.Model.ProductId), ["Product must be in factor's line"]);
            }

            var isDuplicateProduct = await _factorDetailsRepository.IsDuplicateProduct(
                factorHeaderId: request.Model.FactorHeaderId, 
                productId: request.Model.ProductId, 
                cancellationToken: cancellationToken);

            if (isDuplicateProduct)
            {
                throw new SalesApiValidationException(nameof(request.Model.ProductId), ["Duplicate product in a single factor is not allowed"]);
            }

            var entity = new FactorDetailsEntity(
                factorHeaderId: request.Model.FactorHeaderId,
                productId: request.Model.ProductId,
                count: request.Model.Count,
                price: request.Model.Price);

            await _factorDetailsRepository.AddAsync(entity, cancellationToken);
        }
    }
}

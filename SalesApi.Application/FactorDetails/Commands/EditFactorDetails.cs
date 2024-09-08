using FluentValidation;
using MediatR;
using SalesApi.Application.FactorDetails.Dtos;
using SalesApi.Application.Interfaces.Repositories;
using SalesApi.CrossCutting.Exceptions;

namespace SalesApi.Application.FactorDetails.Commands;

public static class EditFactorDetails
{
    public record EditCommand(FactorDetailsEditModel Model) : IRequest;

    public class EditValidator : AbstractValidator<EditCommand>
    {
        public EditValidator()
        {
            RuleFor(x => x.Model.ProductId).NotEmpty();
            RuleFor(x => x.Model.Count).NotEmpty();
            RuleFor(x => x.Model.Price).NotEmpty();
        }
    }

    internal class Handler(IFactorDetailsRepository factorDetailsRepository) : IRequestHandler<EditCommand>
    {
        private readonly IFactorDetailsRepository _factorDetailsRepository = factorDetailsRepository;

        public async Task Handle(EditCommand request, CancellationToken cancellationToken)
        {
            var entity = await _factorDetailsRepository
                .FindAsync(
                id: request.Model.Id,
                cancellationToken: cancellationToken)
                ?? throw new SalesApiNotFoundException("Factor details not found");

            var isHeaderFinal = await _factorDetailsRepository
                .IsHeaderFinal(
                id: request.Model.Id,
                cancellationToken: cancellationToken);

            if (isHeaderFinal)
            {
                throw new SalesApiAppException("Cannot edit a finalized factor");
            }

            entity.ProductId = request.Model.ProductId;
            entity.Count = request.Model.Count;
            entity.Price = request.Model.Price;

            await _factorDetailsRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}

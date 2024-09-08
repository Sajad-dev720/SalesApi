using SalesApi.Application.FactorDetails.Dtos;

namespace SalesApi.Application.Factor.Dtos;

public class FactorHeaderBaseModel
{
    public Guid SaleLineId { get; set; }

    public Guid CustomerId { get; set; }

    public Guid SalePersonId { get; set; }
}

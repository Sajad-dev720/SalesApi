namespace SalesApi.Application.FactorHeader.Dtos;

public class FactorHeaderDetailsModel
{
    public Guid Id { get; set; }

    public string CustomerFullName { get; set; } = null!;

    public string SalesPersonFullName { get; set; } = null!;

    public string SaleLineTitle { get; set; } = null!;
}

namespace SalesApi.Application.FactorDetails.Dtos;

public class FactorDetailsBaseModel
{
    public Guid ProductId { get; set; }

    public int Count { get; set; }

    public long Price { get; set; }
}

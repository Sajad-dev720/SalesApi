namespace SalesApi.Application.FactorDetails.Dtos;

public class FactorDetailsDetailsModel
{
    public Guid Id { get; set; }

    public string ProductTitle { get; set; } = null!;

    public int Count { get; set; }

    public long Price { get; set; }
}

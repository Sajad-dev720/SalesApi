namespace SalesApi.Domain.Entities;

public class SaleLineProductEntity : BaseEntity
{
    public Guid SaleLineId { get; set; }

    public virtual SaleLineEntity? SaleLine { get; set; }

    public Guid ProductId { get; set; }

    public virtual ProductEntity? Product { get; set; }
}

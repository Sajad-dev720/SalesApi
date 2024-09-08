namespace SalesApi.Domain.Entities;

public class SaleLineSalesPersonEntity : BaseEntity
{
    public Guid SaleLineId { get; set; }

    public virtual SaleLineEntity? SaleLine { get; set; }

    public Guid SalesPersonId { get; set; }

    public virtual SalesPersonEntity? SalesPerson { get; set; }
}

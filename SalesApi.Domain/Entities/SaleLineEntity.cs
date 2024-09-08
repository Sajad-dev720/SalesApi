namespace SalesApi.Domain.Entities;

public class SaleLineEntity : BaseEntity
{
    public string Title { get; set; } = null!;

    public virtual ICollection<ProductEntity> Products { get; set; } = [];

    public virtual ICollection<SalesPersonEntity> SalesPeople { get; set;} = [];

    public virtual ICollection<FactorHeaderEntity> SaleLineFactorHeaders { get; set; } = [];
}

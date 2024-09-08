namespace SalesApi.Domain.Entities;

public class ProductEntity : BaseEntity
{
    public string Title { get; set; } = null!;

    public virtual ICollection<SaleLineEntity> SaleLines { get; set; } = [];
    public virtual ICollection<FactorDetailsEntity> ProductFactorDetails { get; set; } = [];
}

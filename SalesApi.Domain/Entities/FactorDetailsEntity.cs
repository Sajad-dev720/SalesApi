namespace SalesApi.Domain.Entities;

public class FactorDetailsEntity : BaseEntity
{
    public FactorDetailsEntity(Guid factorHeaderId, Guid productId, int count, long price)
    {
        FactorHeaderId = factorHeaderId;
        ProductId = productId;
        Count = count;
        Price = price;
    }

    public Guid FactorHeaderId { get; set; }

    public virtual FactorHeaderEntity? FactorHeader { get; set; }

    public Guid ProductId { get; set; }

    public virtual ProductEntity? Product { get; set; }

    public int Count { get; set; }

    public long Price { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<DiscountEntity> FactorDetailsDiscounts { get; set; } = [];
}

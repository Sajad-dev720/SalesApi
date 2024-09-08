using SalesApi.Domain.Enums;

namespace SalesApi.Domain.Entities;

public class DiscountEntity : BaseEntity
{
    public DiscountEntity(Guid factorHeaderId, DiscountType type, Guid? factorDetailsId = null)
    {
        FactorHeaderId = factorHeaderId;
        FactorDetailsId = factorDetailsId;
        Type = type;
    }

    public Guid FactorHeaderId { get; set; }

    public virtual FactorHeaderEntity? FactorHeader { get; set; }

    public Guid? FactorDetailsId { get; set; }

    public virtual FactorDetailsEntity? FactorDetails { get; set; }

    public DiscountType Type { get; set; }

    public long DiscountAmount { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }
}

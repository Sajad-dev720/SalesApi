using SalesApi.Domain.Enums;

namespace SalesApi.Domain.Entities;

public class FactorHeaderEntity : BaseEntity
{
    public FactorHeaderEntity(Guid saleLineId, Guid customerId, Guid salesPersonId)
    {
        SaleLineId = saleLineId;
        CustomerId = customerId;
        SalesPersonId = salesPersonId;
        Status = FactorStatus.Draft;
    }

    public Guid SaleLineId { get; set; }

    public virtual SaleLineEntity? SaleLine { get; set; }

    public Guid CustomerId { get; set; }

    public virtual CustomerEntity? Customer { get; set; }

    public Guid SalesPersonId { get; set; }

    public virtual SalesPersonEntity? SalesPerson { get; set; }

    public FactorStatus Status { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<FactorDetailsEntity> FactorDetails { get; set; } = [];

    public virtual ICollection<DiscountEntity> FactorHeaderDiscounts { get; set; } = [];
}

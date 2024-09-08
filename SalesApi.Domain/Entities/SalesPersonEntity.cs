namespace SalesApi.Domain.Entities;

public class SalesPersonEntity : BaseEntity
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public virtual ICollection<FactorHeaderEntity> SalesPersonFactorHeaders { get; set; } = [];

    public virtual ICollection<SaleLineEntity> SaleLines { get; set; } = [];
}

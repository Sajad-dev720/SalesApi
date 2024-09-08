namespace SalesApi.Domain.Entities;

public class CustomerEntity : BaseEntity
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FullName { get; set; } = null!;
    public virtual ICollection<FactorHeaderEntity> CustomerFactorHeaders { get; set; } = [];
}

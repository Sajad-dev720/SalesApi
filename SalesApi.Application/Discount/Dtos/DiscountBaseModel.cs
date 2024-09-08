using SalesApi.Domain.Enums;

namespace SalesApi.Application.Discount.Dtos;

public class DiscountBaseModel
{
    public Guid FactorHeaderId { get; set; }

    public Guid? FactorDetailsId { get; set; }

    public DiscountType Type { get; set; }

    public long DiscountAmount { get; set; }
}

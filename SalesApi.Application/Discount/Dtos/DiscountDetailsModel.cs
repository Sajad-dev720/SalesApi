using SalesApi.CrossCutting.Extensions;

namespace SalesApi.Application.Discount.Dtos;

public class DiscountDetailsModel : DiscountBaseModel
{
    public Guid Id { get; set; }

    public string TypeTitle => Type.GetEnumDescription() ?? string.Empty;
}

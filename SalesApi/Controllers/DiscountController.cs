using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesApi.Application.Discount.Commands;
using SalesApi.Application.Discount.Dtos;
using SalesApi.Application.Discount.Queries;
using SalesApi.Application.FactorDetails.Commands;
using SalesApi.Application.FactorDetails.Dtos;
using SalesApi.Application.FactorDetails.Queries;

namespace SalesApi.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DiscountController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("/Discount/GetById")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _sender.Send(new GetDiscount.GetQuery(id));

        return Ok(result);
    }

    [HttpGet("/Discount/GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sender.Send(new GetDiscounts.GetAllQuery());

        return Ok(result);
    }

    [HttpPost("/Discount/Add")]
    public async Task<IActionResult> Add(DiscountAddModel model)
    {
        await _sender.Send(new AddDiscount.AddCommand(model));

        return Ok();
    }

    [HttpPost("Discount/Edit")]
    public async Task<IActionResult> Edit(DiscountEditModel model)
    {
        await _sender.Send(new EditDiscount.EditCommand(model));

        return Ok();
    }

    [HttpPost("Discount/Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _sender.Send(new DeleteDiscount.DeleteCommand(id));

        return Ok();
    }
}

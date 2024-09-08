using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesApi.Application.FactorDetails.Commands;
using SalesApi.Application.FactorDetails.Dtos;
using SalesApi.Application.FactorDetails.Queries;

namespace SalesApi.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FactorDetailsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("/FactorDetails/GetById")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _sender.Send(new GetFactorDetails.GetQuery(id));

        return Ok(result);
    }

    [HttpGet("/FactorDetails/GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sender.Send(new GetAllFactorDetails.GetAllQuery());

        return Ok(result);
    }

    [HttpPost("/FactorDetails/Add")]
    public async Task<IActionResult> Add(FactorDetailsAddModel model)
    {
        await _sender.Send(new AddFactorDetails.AddCommand(model));

        return Ok();
    }

    [HttpPost("/FactorDetails/Edit")]
    public async Task<IActionResult> Edit(FactorDetailsEditModel model)
    {
        await _sender.Send(new EditFactorDetails.EditCommand(model));

        return Ok();
    }

    [HttpPost("/FactorDetails/Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _sender.Send(new DeleteFactorDetails.DeleteCommand(id));

        return Ok();
    }
}

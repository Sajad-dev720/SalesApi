using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesApi.Application.Factor.Commands;
using SalesApi.Application.Factor.Dtos;
using SalesApi.Application.FactorHeader.Commands;
using SalesApi.Application.FactorHeader.Dtos;
using SalesApi.Application.FactorHeader.Queries;

namespace SalesApi.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HeaderFactorController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("/FactorHeader/GetById")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _sender.Send(new GetFactorHeader.GetQuery(id));

        return Ok(result);
    }

    [HttpGet("/FactorHeader/GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sender.Send(new GetFactorHeaders.GetAllQuery());

        return Ok(result);
    }

    [HttpPost("/FactorHeader/Add")]
    public async Task<IActionResult> Add(FactorHeaderAddModel model)
    {
        await _sender.Send(new AddFactorHeader.AddCommand(model));

        return Ok();
    }

    [HttpPost("/FactorHeader/Edit")]
    public async Task<IActionResult> Edit(FactorHeaderEditModel model)
    {
        await _sender.Send(new EditFactorHeader.EditCommand(model));

        return Ok();
    }

    [HttpPost("/FactorHeader/Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _sender.Send(new DeleteFactorHeader.DeleteCommand(id));

        return Ok();
    }

    [HttpPost("/FactorHeader/Finalize")]
    public async Task<IActionResult> Finalize(Guid id)
    {
        await _sender.Send(new FinalizeFactor.FinalizeCommand(id));

        return Ok();
    }
}

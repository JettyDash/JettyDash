using Azure.Core;
using Business.Cqrs;
using Microsoft.AspNetCore.Authorization;
using Schemes.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConnectionController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConnectionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[action]")]
    [Authorize(Roles = Constants.Roles.AdminOrPersonnelOrGuest)]
    public async Task<IActionResult> TestConnection([FromBody] CreateUrlConnectionRequest request)
    {
        var command = new CreateUrlConnectionCommand(request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPost("[action]")]
    [Authorize(Roles = Constants.Roles.AdminOrPersonnelOrGuest)]
    public async Task<IActionResult> SaveUrlConnection([FromBody] CreateUrlConnectionRequest request)
    {
        var command = new CreateUrlConnectionCommand(request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPost("[action]")]
    [Authorize(Roles = Constants.Roles.AdminOrPersonnelOrGuest)]
    public async Task<IActionResult> SaveHostConnection([FromBody] CreateHostConnectionRequest request)
    {
        var command = new CreateHostConnectionCommand(request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPut($"[action]/{{connectionId}}")]
    [Authorize(Roles = Constants.Roles.AdminOrPersonnelOrGuest)]
    public async Task<IActionResult> EditUrlConnection(int connectionId, [FromBody] UpdateUrlConnectionRequest request)
    {
        var command = new UpdateUrlConnectionCommand(connectionId, request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut($"[action]/{{connectionId}}")]
    [Authorize(Roles = Constants.Roles.AdminOrPersonnelOrGuest)]
    public async Task<IActionResult> EditHostConnection(int connectionId, [FromBody] UpdateHostConnectionRequest request)
    {
        var command = new UpdateHostConnectionCommand(connectionId, request);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("{{connectionId}}")]
    [Authorize(Roles = Constants.Roles.AdminOrPersonnelOrGuest)]
    public async Task<IActionResult> DeleteConnection(int connectionId)
    {
        var command = new DeleteConnectionCommand(connectionId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("[action]")]
    [Authorize(Roles = Constants.Roles.AdminOrPersonnelOrGuest)]
    public async Task<IActionResult> GetAllConnections()
    {
        var query = new GetAllConnectionQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}

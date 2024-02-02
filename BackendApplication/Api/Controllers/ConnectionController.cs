using Business.Cqrs;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Schemes.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Schemes.Dtos.Pipelines;
using Schemes.Enums;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConnectionController(IMediator mediator, IUserService user) : ControllerBase
{

    [HttpPost("[action]")]
    [Authorize(Roles = Constants.Roles.AdminOrPersonnelOrGuest)]
    public async Task<IActionResult> TestConnection([FromBody] string connectionString, DatabaseType databaseType, CancellationToken cancellationToken)
    {
        var command = new TestConnectionCommand(connectionString, databaseType);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost("[action]")]
    [Authorize(Roles = Constants.Roles.AdminOrPersonnelOrGuest)]
    public async Task<IActionResult> SaveUrlConnection([FromBody] CreateUrlConnectionRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUrlConnectionCommand(request);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost("[action]")]
    [Authorize(Roles = Constants.Roles.AdminOrPersonnelOrGuest)]
    public async Task<IActionResult> SaveHostConnection([FromBody] CreateHostConnectionRequest request, CancellationToken cancellationToken)
    {
        var pipeline = new CreateHostConnectionPipeline(user.GetRole(), user.GetId());
        var command = new CreateHostConnectionCommand(request, pipeline);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpPut($"[action]/{{connectionId}}")]
    [Authorize(Roles = Constants.Roles.AdminOrPersonnelOrGuest)]
    public async Task<IActionResult> EditUrlConnection(int connectionId, [FromBody] UpdateUrlConnectionRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateUrlConnectionCommand(connectionId, request);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPut($"[action]/{{connectionId}}")]
    [Authorize(Roles = Constants.Roles.AdminOrPersonnelOrGuest)]
    public async Task<IActionResult> EditHostConnection(int connectionId, [FromBody] UpdateHostConnectionRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateHostConnectionCommand(connectionId, request);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete("{{connectionId}}")]
    [Authorize(Roles = Constants.Roles.AdminOrPersonnelOrGuest)]
    public async Task<IActionResult> DeleteConnection(int connectionId, CancellationToken cancellationToken)
    {
        var command = new DeleteConnectionCommand(connectionId);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpGet("[action]")]
    [Authorize(Roles = Constants.Roles.AdminOrPersonnelOrGuest)]
    public async Task<IActionResult> GetAllConnections(CancellationToken cancellationToken)
    {
        var pipeline = new GetAllConnectionPipeline(user.GetRole(), user.GetId());
        var query = new GetAllConnectionQuery(pipeline);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}
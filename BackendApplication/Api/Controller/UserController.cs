using System.ComponentModel.DataAnnotations;
using Business.Cqrs;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Schemes.Constant;
using Schemes.Dto;

namespace Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = Constants.Roles.Admin)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var command = new CreateUserCommand(request);
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{userId}")]
    [Authorize(Roles = Constants.Roles.Admin)]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var command = new DeleteUserCommand(userId);
        var result = await mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPatch("[action]")]
    [Authorize(Roles = Constants.Roles.Admin)]
    public async Task<IActionResult> ActivateUser(int UserId)
    {
        var command = new ActivateUserCommand(UserId);
        var result = await mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPatch("[action]")]
    [Authorize(Roles = Constants.Roles.Admin)]
    public async Task<IActionResult> DeactivateUser(int userId)
    {
        var command = new DeactivateUserCommand(userId);
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("[action]")]
    [Authorize(Roles = Constants.Roles.Admin)]
    public async Task<IActionResult> GetAllUser()
    {
        var query = new GetAllUserQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet]
    [Authorize(Roles = Constants.Roles.Admin)]
    public async Task<IActionResult> GetUserById(int userId)
    {
        var query = new GetUserByIdQuery(userId);
        var result = await mediator.Send(query);
        return Ok(result);
    }
}
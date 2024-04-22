using Business.Cqrs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Schemes.DTOs;

namespace Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TokenController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<TokenResponse> CreateTokenAsync([FromBody] TokenRequest request, CancellationToken cancellationToken)
    {
        var operation = new CreateTokenCommand(request);
        var result = await mediator.Send(operation, cancellationToken);
        return result;
    }
    
}
using Business.Cqrs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Schemes.DTOs;

namespace Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IMediator mediator;

    public TokenController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
            
    [HttpPost]
    public async Task<TokenResponse> CreateToken([FromBody] TokenRequest request, CancellationToken cancellationToken)
    {
        var operation = new CreateTokenCommand(request);
        var result = await mediator.Send(operation, cancellationToken);
        return result;
    }
    
}
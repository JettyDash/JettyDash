using Schemes.Dtos;
using Schemes.Mediatr;

namespace Business.Cqrs;

public record CreateTokenCommand(TokenRequest Model) : ICommand<TokenResponse>;
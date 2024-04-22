using Schemes.Dto;
using Schemes.Mediatr;

namespace Business.Cqrs;

public record CreateTokenCommand(TokenRequest Model) : ICommand<TokenResponse>;
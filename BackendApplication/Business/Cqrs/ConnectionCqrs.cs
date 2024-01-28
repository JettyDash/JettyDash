using MediatR;
using Schemes.Dtos;

namespace Business.Cqrs;


// Connection Respose base responsetan kalıtım alacak apiresponse parametrelerini eklersin
public record CreateUrlConnectionCommand(CreateUrlConnectionRequest Model) : IRequest<ConnectionResponse>;
public record CreateHostConnectionCommand(CreateHostConnectionRequest Model) : IRequest<ConnectionResponse>;

public record UpdateUrlConnectionCommand(int ConnectionId, UpdateUrlConnectionRequest Model) : IRequest<ConnectionResponse>;
public record UpdateHostConnectionCommand(int ConnectionId, UpdateHostConnectionRequest Model) : IRequest<ConnectionResponse>;

public record DeleteConnectionCommand(int ConnectionId) : IRequest<ConnectionResponse>;

public record GetAllConnectionQuery() : IRequest<List<ConnectionResponse>>;


using MediatR;
using Schemes.Dtos;

namespace Business.Cqrs;


// TODO: Save etmeden önce mutlaka test endpointini çalıştır.

public record TestConnectionCommand(int ConnectionId) : IRequest<ApiResponse<ConnectionResponse>>;
public record CreateUrlConnectionCommand(CreateUrlConnectionRequest Model) : IRequest<ApiResponse<ConnectionResponse>>;
public record CreateHostConnectionCommand(CreateHostConnectionRequest Model) : IRequest<ApiResponse<ConnectionResponse>>;


// TODO: Check nullable working properly
public record UpdateUrlConnectionCommand(int ConnectionId, UpdateUrlConnectionRequest Model) : IRequest<ApiResponse<ConnectionResponse>>;
public record UpdateHostConnectionCommand(int ConnectionId, UpdateHostConnectionRequest Model) : IRequest<ApiResponse<ConnectionResponse>>;
public record DeleteConnectionCommand(int ConnectionId) : IRequest<ApiResponse<ConnectionResponse>>;


public record GetAllConnectionQuery() : IRequest<ApiResponse<List<ConnectionResponse>>>;


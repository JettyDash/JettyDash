using Schemes.Dto;
using Schemes.Dto.Pipeline;
using Schemes.Enum;
using Schemes.Mediatr;

namespace Business.Cqrs;


public record TestConnectionCommand(TestConnectionRequest Model) : ICommand<ApiResponse<ConnectionResponse>>;
public record CreateUrlConnectionCommand(CreateUrlConnectionRequest Model) : ICommand<ApiResponse<ConnectionResponse>>;
public record CreateHostConnectionCommand(CreateHostConnectionRequest Model, CreateHostConnectionPipeline Context) : ICommand<ApiResponse<ConnectionResponse>>;


// TODO: Check nullable working properly
public record UpdateUrlConnectionCommand(int ConnectionId, UpdateUrlConnectionRequest Model) : ICommand<ApiResponse<ConnectionResponse>>;
public record UpdateHostConnectionCommand(int ConnectionId, UpdateHostConnectionRequest Model) : ICommand<ApiResponse<ConnectionResponse>>;
public record DeleteConnectionCommand(int ConnectionId) : ICommand<ApiResponse<ConnectionResponse>>;


public record GetAllConnectionQuery(GetAllConnectionPipeline Context) : IQuery<ApiResponse<List<ConnectionResponse>>>;


using AutoMapper;
using Business.Cqrs;
using Business.Service;
using Infrastructure.DbContext;
using Infrastructure.Entity;
using Schemes.Dto;
using Schemes.Mediatr;

namespace Business.Command;

public class ConnectionCommandHandler(
    BackendDbContext dbContext,
    IMapper mapper,
    IDapperServiceFactory dapperServiceFactory) :
    IAsyncCommandHandler<TestConnectionCommand, ApiResponse<ConnectionResponse>>,
    IAsyncCommandHandler<CreateUrlConnectionCommand, ApiResponse<ConnectionResponse>>,
    IAsyncCommandHandler<CreateHostConnectionCommand, ApiResponse<ConnectionResponse>>,
    IAsyncCommandHandler<UpdateUrlConnectionCommand, ApiResponse<ConnectionResponse>>,
    IAsyncCommandHandler<UpdateHostConnectionCommand, ApiResponse<ConnectionResponse>>,
    IAsyncCommandHandler<DeleteConnectionCommand, ApiResponse<ConnectionResponse>>
{
    public async Task<ApiResponse<ConnectionResponse>> Handle(TestConnectionCommand request,
        CancellationToken cancellationToken)
    {
        var connection = dapperServiceFactory.Create(request.Model.ConnectionString, request.Model.DatabaseType);

        (bool result, string message) = await connection.TestConnection(cancellationToken);

        return new ApiResponse<ConnectionResponse>(message: message, success: result);
    }

    public async Task<ApiResponse<ConnectionResponse>> Handle(CreateUrlConnectionCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<ConnectionResponse>> Handle(CreateHostConnectionCommand request,
        CancellationToken cancellationToken)
    {
        // Check if connection string is unique NOT IMPLEMENTED
        // await SaveConnectionToVault(request);
        var response = await SaveConnectionToDatabase(request, cancellationToken);

        return new ApiResponse<ConnectionResponse>(response);
    }

    public async Task<ApiResponse<ConnectionResponse>> Handle(UpdateUrlConnectionCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<ConnectionResponse>> Handle(UpdateHostConnectionCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<ConnectionResponse>> Handle(DeleteConnectionCommand request,
        CancellationToken cancellationToken)
    {
        // Check if connection exists if exists delete from vault and database
        throw new NotImplementedException();
    }

    // private async Task SaveConnectionToVault(CreateHostConnectionCommand request)
    // {
    //     var path = string.Format(Constant.VaultPath.Database,
    //         request.Context.Username,
    //         request.Context.VaultIdentifier);
    //
    //     var data = new VaultConnectionData(request.Context.ConnectionString);
    //
    //     await vaultService.SaveOrUpdateCredentials(
    //         path: path,
    //         data: data,
    //         mountPoint: vaultConfig.Value.Mount
    //     );
    // }

    // SaveConnectionToDatabase
    private async Task<ConnectionResponse> SaveConnectionToDatabase(CreateHostConnectionCommand request,
        CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Connection>(request.Model);
        // entity.VaultIdentifier = request.Context.VaultIdentifier;
        entity.UserId = request.Context.UserId;

        await dbContext.Connections.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<ConnectionResponse>(entity);
        return response;
    }
}
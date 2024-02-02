using AutoMapper;
using Business.Cqrs;
using Business.Services;
using Infrastructure.DbContext;
using Infrastructure.Entities;
using Microsoft.Extensions.Options;
using Schemes.DTOs;
using Schemes.Mediatr;
using Schemes.Vault;

namespace Business.Commands;

public class ConnectionCommandHandler(
    IOptions<VaultConfig> vaultConfig,
    BackendDbContext dbContext,
    IMapper mapper,
    IVaultService vaultService,
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
        var connection = dapperServiceFactory.Create(request.ConnectionString, request.DatabaseType);

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
        // save vault
        await SaveConnectionToVault(request);
        // save database
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
    
    private async Task SaveConnectionToVault(CreateHostConnectionCommand request)
    {
        var credentialKey = string.Format(Constants.VaultPath.Database,
            request.Context.UserId,
            request.Context.VaultIdentifier);
            
        Dictionary<string, string> values = new()
        {
            { credentialKey, request.Context.ConnectionString }
        };
        
        await vaultService.SaveOrUpdateCredentials(
            path: vaultConfig.Value.DatabaseSecretsPath,
            values: values,
            mountPoint: vaultConfig.Value.Mount
        );

    }
    
    // SaveConnectionToDatabase
    private async Task<ConnectionResponse> SaveConnectionToDatabase(CreateHostConnectionCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Connection>(request.Model);
        entity.VaultIdentifier = request.Context.VaultIdentifier;
        entity.UserId = request.Context.UserId;

        await dbContext.Connections.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        var response = mapper.Map<ConnectionResponse>(entity);
        return response;
    }
}

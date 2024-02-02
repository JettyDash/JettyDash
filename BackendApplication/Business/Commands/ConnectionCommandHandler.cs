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
        /* Context sharing, tamam
         pipeline behavior,  tamam
         contextinitializer, tamam, string to enum tamam
         common: dbtransactionbehaviour
         atomic transaction(openbehavior) and rollback  dbtransactionbehaviour https://www.youtube.com/watch?v=kamcg-KreJE&ab_channel=Codewrinkles
        
        */
        
    
        // başarılı olursa  connected olmazsa kaydetme hiçbir yere
        // check credentials in vault and  CreateConnectionStringFromUrl
        /*
         * check null and enum
         * User can only create connection for himself
         * create unique db id and use it for fluentvalidation also
         */

        
        // save vault
        // await vaultService.GetCredentialByPath(path:"DatabaseCredentials", mountPoint: "secret");

        await vaultService.SaveOrUpdateCredentials(
            path: vaultConfig.Value.DatabaseSecretsPath,
            values: CreateCredentialDictionary(request.Context.UserId, request.Context.VaultIdentifier, request.Context.ConnectionString),
            mountPoint: vaultConfig.Value.Mount
        );

        var entity = mapper.Map<Connection>(request.Model);
        entity.VaultIdentifier = request.Context.VaultIdentifier;
        entity.UserId = request.Context.UserId;

        // save db
        await dbContext.Connections.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<ConnectionResponse>(entity);

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
        throw new NotImplementedException();
    }
    
    private Dictionary<string, string> CreateCredentialDictionary(int userId, string vaultIdentifier,
        string connectionString)
    {
        string credentialKey = string.Format(Constants.VaultPath.Database, userId, vaultIdentifier);
        Dictionary<string, string> values = new() { { credentialKey, connectionString } };

        return values;
    }
}

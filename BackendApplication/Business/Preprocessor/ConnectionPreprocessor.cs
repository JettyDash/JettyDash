using Business.Cqrs;
using MediatR;
using Schemes.DTOs;
using Schemes.Enums;
using Schemes.Exceptions;

namespace Business.Preprocessor;

public class CreateHostConnectionPipelineInitializer<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : CreateHostConnectionCommand

{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    { 
        request.Context.DatabaseType = Enum.Parse<DatabaseType>(request.Model.DatabaseType);
        request.Context.ConnectionString = CreateConnectionStringFromUrl(request.Model, request.Context.DatabaseType );
        // request.Context.VaultIdentifier = Guid.NewGuid().ToString().ToUpper();

        return next();

    }
    
    private string CreateConnectionStringFromUrl(CreateHostConnectionRequest model, DatabaseType databaseType)
    {
        return databaseType switch
        {
            // Only SQL Authentication mode is supported
            // TODO: trust server certificate true to false
            DatabaseType.SqlServer =>
                $"Server={model.Host},{model.Port};Database={model.DatabaseName};User Id={model.Username};Password={model.Password};Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Timeout=5;Connection Lifetime=180;" +
                "Integrated Security=false;Encrypt=true;TrustServerCertificate=true;MultipleActiveResultSets=true;",

            DatabaseType.MySql =>
                $"Server={model.Host};Port={model.Port};Database={model.DatabaseName};Uid={model.Username};Pwd={model.Password}",
                //;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Timeout=5;Connection Lifetime=180
            DatabaseType.PostgresSql =>
                $"User ID={model.Username};Password={model.Password};Host={model.Host};Port={model.Port};Database={model.DatabaseName};Pooling=true;Minimum Pool Size=0;Maximum Pool Size=100;Connection Lifetime=180;",
            DatabaseType.Oracle =>
                // $"User ID={model.Username};Password={model.Password};Host={model.Host};Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;",
                $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={model.Host})(PORT={model.Port})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={model.DatabaseName})));User Id={model.Username};Password={model.Password};Pooling=True;Connection Timeout=5;Connection Lifetime=180;",
            _ => throw new ArgumentOutOfRangeException(nameof(databaseType), databaseType, null)
        };
    }
}


public class CreateHostConnectionValidationBehaviour<TRequest, TResponse>(IMediator mediator)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : CreateHostConnectionCommand

{
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Check if connection string is valid
        var testConnectionCommand =
            new TestConnectionCommand(new TestConnectionRequest
            {
                ConnectionString = request.Context.ConnectionString, 
                DatabaseType = request.Context.DatabaseType
            });
        var result = await mediator.Send(testConnectionCommand, cancellationToken);

        if (result.Success == false)
        {
            throw new HttpException(result.Message, 400);
        }
        
        // Check if connection is unique
        // get all connection strings of user in vault and compare with new one
        
        return await next();

    }
}



using AutoMapper;
using Business.Cqrs;
using Business.Services;
using Business.Validators;
using Infrastructure.DbContext;
using Infrastructure.Entities;
using MediatR;
using Schemes.Dtos;
using Schemes.Enums;
using Schemes.Exceptions;

namespace Business.Commands;

public class ConnectionCommandHandler(
    BackendDbContext dbContext,
    IMapper mapper,
    IHandlerValidator validate,
    IVaultService vaultService,
    IUserService userService,
    IMediator mediator,
    IDapperServiceFactory dapperServiceFactory) :
    IRequestHandler<TestConnectionCommand, ApiResponse<ConnectionResponse>>,
    IRequestHandler<CreateUrlConnectionCommand, ApiResponse<ConnectionResponse>>,
    IRequestHandler<CreateHostConnectionCommand, ApiResponse<ConnectionResponse>>,
    IRequestHandler<UpdateUrlConnectionCommand, ApiResponse<ConnectionResponse>>,
    IRequestHandler<UpdateHostConnectionCommand, ApiResponse<ConnectionResponse>>,
    IRequestHandler<DeleteConnectionCommand, ApiResponse<ConnectionResponse>>
{

    public async Task<ApiResponse<ConnectionResponse>> Handle(TestConnectionCommand request,
        CancellationToken cancellationToken)
    {
        var connection = dapperServiceFactory.Create(request.ConnectionString, request.DatabaseType);
      
        bool result = await connection.TestConnection(cancellationToken);
        
        if (result)
        {
            return new ApiResponse<ConnectionResponse>("Connected established successfully");
        }
        else
        {
            return new ApiResponse<ConnectionResponse>("Failed to establish connection");
        }
        
    }

    public async Task<ApiResponse<ConnectionResponse>> Handle(CreateUrlConnectionCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<ConnectionResponse>> Handle(CreateHostConnectionCommand request,
        CancellationToken cancellationToken)
    {
        /*
         * check null and enum
         * User can only create connection for himself
         * create unique db id and use it for fluentvalidation also
         */
        int userId = userService.GetUserId();
        string vaultIdentifier = Guid.NewGuid().ToString();
        DatabaseType databaseType = Enum.Parse<DatabaseType>(request.Model.DatabaseType);
        //create connection string
        string connectionString = CreateConnectionStringFromUrl(model: request.Model, databaseType: databaseType);

        // test connection
        var result = await mediator.Send(new TestConnectionCommand(connectionString, databaseType), cancellationToken);
        
        if (result.Success == false)
        {
            throw new HttpException(result.Message, 400);
        }
        
        // save vault
        await vaultService.SaveOrUpdateCredentials("users/" + userId + "/databases/" + vaultIdentifier,connectionString);
        
        var entity = mapper.Map<Connection>(request.Model);
        entity.VaultIdentifier = vaultIdentifier;
        entity.UserId = userId;
        
        // save db
        await dbContext.Connections.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        // if success save once vault sonra db
        // if false save db

        // başarılı olursa  connected olmazsa kaydetme hiçbir yere
        
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

    /// <summary>
    /// Connection Timeout means the time to wait (in seconds) while trying to establish a connection before terminating the attempt and generating an error. Connection Lifetime means the time (in seconds) to wait before closing idle connections.
    /// </summary>
    private string CreateConnectionStringFromUrl(CreateHostConnectionRequest model, DatabaseType databaseType)
    {
        return databaseType switch
        {
            DatabaseType.SqlServer =>
                $"Server={model.Host},{model.Port};Database={model.DatabaseOrSchema};User Id={model.Username};Password={model.Password};Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Timeout=5;Connection Lifetime=180;",
            DatabaseType.MySql =>
                $"Server={model.Host};Port={model.Port};Database={model.DatabaseOrSchema};Uid={model.Username};Pwd={model.Password};Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Timeout=5;Connection Lifetime=180;",
            DatabaseType.PostgreSql =>
                $"User ID={model.Username};Password={model.Password};Host={model.Host};Port={model.Port};Database={model.DatabaseOrSchema};Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;",
            DatabaseType.Oracle =>
                // $"User ID={model.Username};Password={model.Password};Host={model.Host};Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;",
                $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={model.Host})(PORT={model.Port})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={model.DatabaseOrSchema})));User Id={model.Username};Password={model.Password};Pooling=True;Connection Timeout=5;Connection Lifetime=180;",
            _ => throw new ArgumentOutOfRangeException(nameof(databaseType), databaseType, null)
        };
    }
}

/*
 * POSTGRESQL
 * User ID=root;Password=myPassword;Host=localhost;Port=5432;Database=myDataBase;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;
 * User ID=myUsername;Password=myPassword;Host=ora;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;
 */
/*
 *
 *
 * using (var transaction = connection.BeginTransaction())
   {
       try
       {
           connection.Execute("INSERT INTO Employees (Name, Age) VALUES (@Name, @Age)", new { Name = "John", Age = 30 }, transaction);
           connection.Execute("INSERT INTO Employees (Name, Age) VALUES (@Name, @Age)", new { Name = "Jane", Age = 35 }, transaction);
           transaction.Commit();
       }
       catch
       {
           transaction.Rollback();
           throw;
       }
   }
 */
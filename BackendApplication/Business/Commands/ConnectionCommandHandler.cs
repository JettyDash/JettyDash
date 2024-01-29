using AutoMapper;
using Business.Cqrs;
using Business.Services;
using Business.Validators;
using Infrastructure.DbContext;
using Infrastructure.Entities;
using MediatR;
using Schemes.Dtos;
using Schemes.Enums;

namespace Business.Commands;

public class ConnectionCommandHandler(
    BackendDbContext dbContext,
    IMapper mapper,
    IHandlerValidator validate,
    IVaultService vaultService,
    IUserService userService,
    IDapperService dapperService)
    :
        IRequestHandler<TestConnectionCommand, ApiResponse<ConnectionResponse>>,
        IRequestHandler<CreateUrlConnectionCommand, ApiResponse<ConnectionResponse>>,
        IRequestHandler<CreateHostConnectionCommand, ApiResponse<ConnectionResponse>>,
        IRequestHandler<UpdateUrlConnectionCommand, ApiResponse<ConnectionResponse>>,
        IRequestHandler<UpdateHostConnectionCommand, ApiResponse<ConnectionResponse>>,
        IRequestHandler<DeleteConnectionCommand, ApiResponse<ConnectionResponse>>
{
    private readonly BackendDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly IHandlerValidator _validate = validate;
    private readonly IVaultService _vaultService = vaultService;
    private readonly IUserService _userService = userService;
    private readonly IDapperService _dapperService = dapperService;


    public async Task<ApiResponse<ConnectionResponse>> Handle(TestConnectionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<ConnectionResponse>> Handle(CreateUrlConnectionCommand request, CancellationToken cancellationToken)
    {
        /*
         * check null and enum
         * User can only create connection for himself
         * create unique db id and use it for fluentvalidation also
         */

        string vaultIdentifier = Guid.NewGuid().ToString();
        
        
        // create connection string
        // test connection
        // if success save once vault sonra db
        // if false save db
        
        // başarılı olursa  connected olmazsa failed olur
        
        string connectionString = _vaultService.CreateConnectionString(request.Model);
        _dapperService.Create()

        // var entity = new Connection
        // {
        //     VaultIdentifier = VaultIdentifier,
        //     ConnectionType = ConnectionType.Url,
        //     UserId = userService.GetUserId(),
        //     DatabaseName = request.Model.DatabaseName,
        //     DatabaseType = request.Model.DatabaseType,
        //     Status = ConnectionStatus.Connected,
        //     CreationDate = DateTime.Now,
        //     LastUpdateTime = DateTime.Now,
        // };

        return new ApiResponse<ConnectionResponse>(new ConnectionResponse() { Status = "Connected" });
    }

    public async Task<ApiResponse<ConnectionResponse>> Handle(CreateHostConnectionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<ConnectionResponse>> Handle(UpdateUrlConnectionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<ConnectionResponse>> Handle(UpdateHostConnectionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<ConnectionResponse>> Handle(DeleteConnectionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private string CreateConnectionStringFromUrl()
    {
        string skeletalConnectionString = "Server={0};Database={1};User Id={2};Password={3};{4}";
        string poolingParameter = "Pooling=true;";
        timeout = "Connection Timeout=30;";
    }
    
    private static CreateConnectionStringFromHost()
    {
        
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
using System.Security.Cryptography;
using System.Text;
using Business.Cqrs;
using Business.Validators;
using AutoMapper;
using Infrastructure.DbContext;
using Infrastructure.Entities;
using Schemes.DTOs;
using Schemes.Mediatr;

namespace Business.Commands;

public class UserCommandHandler :
    IAsyncCommandHandler<CreateUserCommand, ApiResponse<UserResponse>>,
    IAsyncCommandHandler<DeleteUserCommand, ApiResponse<UserResponse>>,
    IAsyncCommandHandler<ActivateUserCommand, ApiResponse<UserResponse>>,
    IAsyncCommandHandler<DeactivateUserCommand, ApiResponse<UserResponse>>
{
    private readonly BackendDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IHandlerValidator validate;

    public UserCommandHandler(
        BackendDbContext dbContext,
        IMapper mapper,
        IHandlerValidator validator)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.validate = validator;
    }

    public async Task<ApiResponse<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Add User to Vault as username
        await validate.RecordNotExistAsync<User>(x => x.Username == request.Model.Username, cancellationToken);
        await validate.RecordNotExistAsync<User>(x => x.Email == request.Model.Email, cancellationToken);
        
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] data = Encoding.UTF8.GetBytes(request.Model.Password.Trim());
            byte[] hashBytest = sha256.ComputeHash(data);
            var hashString = BitConverter.ToString(hashBytest).Replace("-", "");
            request.Model.Password = hashString;

        }
        
        var entity = mapper.Map<CreateUserRequest, User>(request.Model);

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<User, UserResponse>(entityResult.Entity);
        return new ApiResponse<UserResponse>(mapped);
    }

    public async Task<ApiResponse<UserResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        // Delete Vault User
        await validate.IdGreaterThanZeroAsync(request.UserId, cancellationToken);
        
        var fromdb = await validate.RecordExistAsync<User>(x => x.UserId == request.UserId, cancellationToken);
        
        fromdb.IsActive = false;
        dbContext.Remove(fromdb);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<User, UserResponse>(fromdb);
        return new ApiResponse<UserResponse>(mapped);
    }

        public async Task<ApiResponse<UserResponse>> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        await validate.IdGreaterThanZeroAsync(request.UserId, cancellationToken);
        
        var fromdb = await validate.RecordExistAsync<User>(x => x.UserId == request.UserId, cancellationToken);
        
        fromdb.IsActive = true;
        await dbContext.SaveChangesAsync(cancellationToken);
        
        var mapped = mapper.Map<User, UserResponse>(fromdb);
        return new ApiResponse<UserResponse>(mapped);

    }

    public async Task<ApiResponse<UserResponse>> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        await validate.IdGreaterThanZeroAsync(request.UserId, cancellationToken);
        
        var fromdb = await validate.RecordExistAsync<User>(x => x.UserId == request.UserId, cancellationToken);
        
        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        
        var mapped = mapper.Map<User, UserResponse>(fromdb);
        return new ApiResponse<UserResponse>(mapped);
    }
}
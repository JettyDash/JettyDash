using Schemes.Dto;
using Schemes.Mediatr;

namespace Business.Cqrs;

public record CreateUserCommand(CreateUserRequest Model) : ICommand<ApiResponse<UserResponse>>;
public record DeleteUserCommand(int UserId) : ICommand<ApiResponse<UserResponse>>;
public record ActivateUserCommand(int UserId) :  ICommand<ApiResponse<UserResponse>>;
public record DeactivateUserCommand(int UserId) :  ICommand<ApiResponse<UserResponse>>;

public record GetAllUserQuery() : IQuery<ApiResponse<List<UserResponse>>>;
public record GetUserByIdQuery(int UserId) :  IQuery<ApiResponse<UserResponse>>;
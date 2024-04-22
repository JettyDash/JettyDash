using Business.Cqrs;
using AutoMapper;
using Business.Validators;
using Infrastructure.DbContext;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Schemes.DTOs;
using Schemes.Exceptions;
using Schemes.Mediatr;

namespace Business.Queries;
public class UserQueryHandler(BackendDbContext dbContext, IMapper mapper, IHandlerValidator validate)
    :
        IAsyncQueryHandler<GetAllUserQuery, ApiResponse<List<UserResponse>>>,
        IAsyncQueryHandler<GetUserByIdQuery, ApiResponse<UserResponse>>
{
    public async Task<ApiResponse<List<UserResponse>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<User>().ToListAsync(cancellationToken);
        
        if (list.Count == 0)
        {
            throw new HttpException(Constants.ErrorMessages.NoRecordFound, 404);
        }
        
        var mappedList = mapper.Map<List<User>, List<UserResponse>>(list);
        return new ApiResponse<List<UserResponse>>(mappedList);
    }

    public async Task<ApiResponse<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        await validate.IdGreaterThanZeroAsync(request.UserId, cancellationToken);
        var entity = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);
        
        if (entity == null)
        {
            throw new HttpException($"Record {request.UserId} not found", 404);
        }
        
        var mapped = mapper.Map<User, UserResponse>(entity);
        return new ApiResponse<UserResponse>(mapped);
    }
}
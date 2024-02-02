using System.Linq.Expressions;
using Business.Services;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Schemes.Exceptions;

namespace Business.Validators;


public interface IHandlerValidator
{
    
    Task<T> RecordExistAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) where T : class;
    
    Task<bool> RecordNotExistAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) where T : class;
    
    Task<bool> IdGreaterThanZeroAsync(int id, CancellationToken cancellationToken);
}


public class HandlerValidator(BackendDbContext dbContext, IUserService userService) : IHandlerValidator
{
    private readonly IUserService _userService = userService;

    public async Task<T> RecordExistAsync<T>(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken) where T : class
    {
        T? entity = await dbContext.Set<T>().FirstOrDefaultAsync<T>(predicate, cancellationToken);
    
        if (entity == null)
        {
            throw new HttpException($"No record found in {predicate.Parameters[0].Type.FullName}", 404);
        }
    
        return entity;
    }
    
    public async Task<bool> RecordNotExistAsync<T>(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken) where T : class
    {
        T? entity = await dbContext.Set<T>().FirstOrDefaultAsync<T>(predicate, cancellationToken);
        
        if (entity != null)
        {
            throw new HttpException($"Existing record in {typeof(T).Name}", 409);
        }
    
        return true;
    }
    
    public Task<bool> IdGreaterThanZeroAsync(int id, CancellationToken cancellationToken)    
    {
        if (id <= 0)
        {
            throw new HttpException(Constants.ErrorMessages.IdLessThanZero, 400);
        }

        return Task.FromResult(true);
    }
}


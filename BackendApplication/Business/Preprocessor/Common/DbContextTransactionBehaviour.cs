using Business.Cqrs;
using Infrastructure.DbContext;
using MediatR;
using Schemes.Exceptions;
using Schemes.Mediatr;

namespace Business.Preprocessor.Common;

public class DbContextTransactionBehaviour<TRequest, TResponse>(BackendDbContext dbContext)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not CreateTokenCommand)
        {
            await using var transaction = dbContext.Database.CurrentTransaction ?? 
                                          await dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await next();
                await transaction.CommitAsync(cancellationToken);
                return response;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new HttpException($"Transaction failed: {e.Message}", 500);
            }
        }
        return await next();
    }
}
using Business.Cqrs;
using MediatR;
using Schemes.Mediatr;

namespace Business.Preprocessor.Common;

public class DbContextTransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    // TODO: Only commands must be transactional
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not CreateTokenCommand)
        {
        }
        return next();
    }
}
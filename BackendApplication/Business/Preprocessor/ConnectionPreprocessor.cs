using Business.Cqrs;
using MediatR;

namespace Business.Preprocessor;

public class CreateHostConnectionCommandPreprocessor1<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : CreateHostConnectionCommand

{
    // TODO: LEFT https://www.jimmybogard.com/sharing-context-in-mediatr-pipelines/

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Console.WriteLine("CreateHostConnectionCommandPreprocessor1 worked)");
        return next();

    }
}


public class CreateHostConnectionCommandPreprocessor2<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : CreateHostConnectionCommand

{
    

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Console.WriteLine("CreateHostConnectionCommandPreprocessor2 worked)");
        return next();

    }
}


// public class CreateHostConnectionCommandPreprocessor3<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//
// {
//     
//
//     public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//     {
//         Console.WriteLine("CreateHostConnectionCommandPreprocessor2 worked)");
//         return next();
//
//     }
// }

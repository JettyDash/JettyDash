using MediatR;

namespace Schemes.Mediatr;

/*// interface ICommand<out TCommandResult>: IRequest<TCommandResult> { }
// interface ICommandHandler<in TCommand, TCommandResult> : IRequestHandler<TCommand, TCommandResult> where TCommand : ICommand<TCommandResult> { }
//
// interface IQuery<out TIQueryResult> : IRequest<TIQueryResult> { }
// interface IQueryHandler<in TQuery, TQueryResult> : IRequestHandler<TQuery, TQueryResult> where TQuery : IQuery<TQueryResult> { }
//



/// <summary>
/// Marker interface to represent a command
/// </summary>
public interface ICommand
{

}

/// <summary>
/// Defines a synchronous handler for a command with a void response.
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    /// <summary>
    /// Handles a command synchronously
    /// </summary>
    /// <param name="command">The command</param>
    /// <returns>void</returns>
    void Handle(TCommand command);
}

/// <summary>
/// Defines an asynchronous handler for a command with a void response.
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
public interface IAsyncCommandHandler<in TCommand> where TCommand : ICommand
{
    /// <summary>
    /// Handles a command asynchronously
    /// </summary>
    /// <param name="command">The command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The task result no response</returns>
    Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}

/// <summary>
/// Send a command through the mediator pipeline to be handled by a single handler.
/// </summary>
public interface ICommandDispatcher
{
    /// <summary>
    /// Synchronously send a command to a single handler
    /// </summary>
    /// <param name="command">command object</param>
    /// <returns>void</returns>
    void Send(ICommand command);
}

/// <summary>
/// Send a command through the mediator pipeline to be handled by a single handler.
/// </summary>
public interface IAsyncCommandDispatcher
{
    /// <summary>
    /// Asynchronously send a command to a single handler
    /// </summary>
    /// <param name="command">command object</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>The task result no response</returns>
    Task SendAsync(ICommand command, CancellationToken cancellationToken = default);
}

/// <summary>
/// Marker interface to represent a query with a result
/// </summary>
/// <typeparam name="TResult">Result type</typeparam>
public interface IQuery<out TResult>
{

}

/// <summary>
/// Defines a synchronous handler for a query
/// </summary>
/// <typeparam name="TQuery">The type of query being handled</typeparam>
/// <typeparam name="TResult">The type of response from the handler</typeparam>
public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
{
    /// <summary>
    /// Handles a query synchronously
    /// </summary>
    /// <param name="query">The query</param>
    /// <returns>Result for the query</returns>
    TResult Handle(TQuery query);
}

/// <summary>
/// Defines an asynchronous handler for a query
/// </summary>
/// <typeparam name="TQuery">The type of query being handled</typeparam>
/// <typeparam name="TResult">The type of response from the handler</typeparam>
public interface IAsyncQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
{
    /// <summary>
    /// Handles a query asynchronously
    /// </summary>
    /// <param name="query">The query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result for the query</returns>
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
}

/// <summary>
/// Send a query through the mediator pipeline to be handled by a single handler.
/// </summary>
public interface IQueryDispatcher
{
    /// <summary>
    /// Synchronously send a query to a single handler
    /// </summary>
    /// <typeparam name="TResult">Response type</typeparam>
    /// <param name="query">query object</param>
    /// <returns>The result contains the handler response</returns>
    TResult Send<TResult>(IQuery<TResult> query);
}

/// <summary>
/// Send a query through the mediator pipeline to be handled by a single handler.
/// </summary>
public interface IAsyncQueryDispatcher
{
    /// <summary>
    /// Asynchronously send a query to a single handler
    /// </summary>
    /// <typeparam name="TResult">Response type</typeparam>
    /// <param name="query">query object</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains the handler response</returns>
    Task<TResult> SendAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}*/


/// <summary>
/// Marker interface to represent a command
/// </summary>
public interface ICommand<out TCommandResult> : IRequest<TCommandResult>
{

}

/// <summary>
/// Defines a synchronous handler for a command with a void response.
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
/// <typeparam name="TCommandResult">The type of response from the handler</typeparam>
public interface ICommandHandler<in TCommand, TCommandResult> : IRequestHandler<TCommand, TCommandResult> where TCommand : ICommand<TCommandResult>
{
    /// <summary>
    /// Handles a command synchronously
    /// </summary>
    /// <param name="command">The command</param>
    /// <returns>Result for the command</returns>
    TCommandResult Handle(TCommand command);
}

/// <summary>
/// Defines an asynchronous handler for a command with a void response.
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
/// <typeparam name="TCommandResult">The type of response from the handler</typeparam>
public interface IAsyncCommandHandler<in TCommand, TCommandResult> : IRequestHandler<TCommand, TCommandResult> where TCommand : ICommand<TCommandResult>
{
    /// <summary>
    /// Handles a command asynchronously
    /// </summary>
    /// <param name="command">The command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The task result for the command</returns>
    Task<TCommandResult> Handle(TCommand command, CancellationToken cancellationToken);
}

/// <summary>
/// Marker interface to represent a query with a result
/// </summary>
/// <typeparam name="TQueryResult">Result type</typeparam>
public interface IQuery<out TQueryResult> : IRequest<TQueryResult>
{

}

/// <summary>
/// Defines a synchronous handler for a query
/// </summary>
/// <typeparam name="TQuery">The type of query being handled</typeparam>
/// <typeparam name="TQueryResult">The type of response from the handler</typeparam>
public interface IQueryHandler<in TQuery, TQueryResult> : IRequestHandler<TQuery, TQueryResult> where TQuery : IQuery<TQueryResult>
{
    /// <summary>
    /// Handles a query synchronously
    /// </summary>
    /// <param name="query">The query</param>
    /// <returns>Result for the query</returns>
    TQueryResult Handle(TQuery query);
}

/// <summary>
/// Defines an asynchronous handler for a query
/// </summary>
/// <typeparam name="TQuery">The type of query being handled</typeparam>
/// <typeparam name="TQueryResult">The type of response from the handler</typeparam>
public interface IAsyncQueryHandler<in TQuery, TQueryResult> : IRequestHandler<TQuery, TQueryResult> where TQuery : IQuery<TQueryResult>
{
    /// <summary>
    /// Handles a query asynchronously
    /// </summary>
    /// <param name="query">The query</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result for the query</returns>
    Task<TQueryResult> Handle(TQuery query, CancellationToken cancellationToken);
}

/// <summary>
/// Send a command through the mediator pipeline to be handled by a single handler.
/// </summary>
public interface ICommandDispatcher<TCommandResult>
{
    /// <summary>
    /// Synchronously send a command to a single handler
    /// </summary>
    /// <param name="command">command object</param>
    /// <returns>void</returns>
    TCommandResult Send(ICommand<TCommandResult> command);
}
/// <summary>
/// Send a command through the mediator pipeline to be handled by a single handler.
/// </summary>
public interface IAsyncCommandDispatcher<TCommandResult>
{
    /// <summary>
    /// Asynchronously send a command to a single handler
    /// </summary>
    /// <param name="command">command object</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>The task result no response</returns>
    Task<TCommandResult> SendAsync(ICommand<TCommandResult> command, CancellationToken cancellationToken = default);
}

/// <summary>
/// Send a query through the mediator pipeline to be handled by a single handler.
/// </summary>
public interface IQueryDispatcher<TQueryResult>
{
    /// <summary>
    /// Synchronously send a query to a single handler
    /// </summary>
    /// <typeparam name="TResult">Response type</typeparam>
    /// <param name="query">query object</param>
    /// <returns>The result contains the handler response</returns>
    TQueryResult Send(IQuery<TQueryResult> query);
}

/// <summary>
/// Send a query through the mediator pipeline to be handled by a single handler.
/// </summary>
public interface IAsyncQueryDispatcher<TQueryResult>
{
    /// <summary>
    /// Asynchronously send a query to a single handler
    /// </summary>
    /// <typeparam name="TResult">Response type</typeparam>
    /// <param name="query">query object</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task that represents the send operation. The task result contains the handler response</returns>
    Task<TQueryResult> SendAsync(IQuery<TQueryResult> query, CancellationToken cancellationToken = default);
}
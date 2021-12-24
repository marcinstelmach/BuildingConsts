namespace BuildingCosts.Shared.Application;

public interface IHandlerDispatcher
{
    Task<TResult> DispatchQueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;

    Task<TResult> DispatchCommandAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;

    Task DispatchCommandAsync<TCommand>(TCommand command) where TCommand : ICommand;
}
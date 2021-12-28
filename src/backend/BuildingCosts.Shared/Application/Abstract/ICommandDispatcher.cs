namespace BuildingCosts.Shared.Application.Abstract;

public interface ICommandDispatcher
{
    Task<TResult> DispatchCommandAsync<TCommand, TResult>(TCommand command) where TCommand : class, ICommand<TResult>;
}
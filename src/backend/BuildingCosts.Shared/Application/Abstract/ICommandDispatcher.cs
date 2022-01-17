namespace BuildingCosts.Shared.Application.Abstract;

public interface ICommandDispatcher
{
    Task<TResult> DispatchCommandAsync<TResult>(ICommand<TResult> command);
}
namespace BuildingCosts.Shared.Application.Abstract;

public interface IQueryDispatcher
{
    Task<TResult> DispatchQueryAsync<TResult>(IQuery<TResult> query);
}
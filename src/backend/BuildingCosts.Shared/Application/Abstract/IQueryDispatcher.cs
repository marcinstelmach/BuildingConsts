namespace BuildingCosts.Shared.Application.Abstract;

public interface IQueryDispatcher
{
    Task<TResult> DispatchQueryAsync<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>;
}
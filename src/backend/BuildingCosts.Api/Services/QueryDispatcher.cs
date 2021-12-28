using System;
using System.Threading.Tasks;
using BuildingCosts.Shared.Application.Abstract;
using Dawn;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingCosts.Api.Services;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> DispatchQueryAsync<TQuery, TResult>(TQuery query)
        where TQuery : class, IQuery<TResult>
    {
        Guard.Argument(query, nameof(query)).NotNull();

        var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await handler.HandleAsync(query);
    }
}
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

    public async Task<TResult> DispatchQueryAsync<TResult>(IQuery<TResult> query)
    {
        Guard.Argument(query, nameof(query)).NotNull();

        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = _serviceProvider.GetService(handlerType);
        var handleMethod = handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync));
        return await (Task<TResult>)handleMethod.Invoke(handler, new object[] { query });
    }
}
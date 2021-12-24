using System;
using System.Threading.Tasks;
using BuildingCosts.Shared.Application;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingCosts.Api;

public class HandlerDispatcher : IHandlerDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public HandlerDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> DispatchQueryAsync<TQuery, TResult>(TQuery query)
        where TQuery : IQuery<TResult>
    {
        var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await handler.HandleAsync(query);
    }

    public async Task<TResult> DispatchCommandAsync<TCommand, TResult>(TCommand command)
        where TCommand : ICommand<TResult>
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
        return await handler.HandleAsync(command);
    }

    public async Task DispatchCommandAsync<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.HandleAsync(command);
    }
}
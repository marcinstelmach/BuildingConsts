using System;
using System.Threading.Tasks;
using BuildingCosts.Shared.Application.Abstract;
using Dawn;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingCosts.Api.Services;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> DispatchCommandAsync<TCommand, TResult>(TCommand command)
        where TCommand : class, ICommand<TResult>
    {
        Guard.Argument(command, nameof(command)).NotNull();

        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
        return await handler.HandleAsync(command);
    }
}
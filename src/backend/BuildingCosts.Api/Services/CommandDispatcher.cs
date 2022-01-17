using System;
using System.Threading.Tasks;
using BuildingCosts.Shared.Application.Abstract;
using Dawn;

namespace BuildingCosts.Api.Services;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task<TResult> DispatchCommandAsync<TResult>(ICommand<TResult> command)
    {
        Guard.Argument(command, nameof(command)).NotNull();

        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
        var handler = _serviceProvider.GetService(handlerType);
        var handleMethod = handlerType.GetMethod(nameof(ICommandHandler<ICommand<TResult>, TResult>.HandleAsync));
        return await (Task<TResult>)handleMethod.Invoke(handler, new object[] { command });
    }
}
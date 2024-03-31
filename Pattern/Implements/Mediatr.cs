using Microsoft.Extensions.DependencyInjection;
using Pattern.Interfaces;
using System.Collections.Concurrent;

namespace Pattern.Implements;
public class Mediatr(IServiceProvider serviceProvider, ConcurrentDictionary<Type, Type> handlerDetails) : IMediatr
{
    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> command)
    {
        var requestType = command.GetType();

        if (!handlerDetails.ContainsKey(requestType)) throw new Exception($"This exception throw by {requestType.Name}");

        handlerDetails.TryGetValue(requestType, out var requestHandlerType);

        var handlerImplement = serviceProvider.GetRequiredService(requestHandlerType!);

        return await (Task<TResponse>)handlerImplement.GetType()
                                             .GetMethod("Handler")!
                                             .Invoke(handlerImplement, new object[] { command })!;

    }
}

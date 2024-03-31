using Pattern.Interfaces;
using System.Collections.Concurrent;

namespace Pattern.Implements;
public class Mediatr(Func<Type, object> serviceResolver, ConcurrentDictionary<Type, Type> handlerDetails) : IMediatr
{
    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> command)
    {
        var requestType = command.GetType();

        if (!handlerDetails.ContainsKey(requestType)) throw new Exception($"This exception throw by {requestType.Name}");

        handlerDetails.TryGetValue(requestType, out var requestHandlerType);

        var handler = serviceResolver.Invoke(requestHandlerType!);

        return await (Task<TResponse>)handler.GetType()
                                             .GetMethod("Handler")!
                                             .Invoke(handler, new object[] { command })!;

    }
}

namespace Pattern.Interfaces;
public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handler(TRequest request);
}

namespace Pattern.Interfaces;
public interface IMediatr
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> command);
}

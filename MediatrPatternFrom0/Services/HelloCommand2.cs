using Pattern.Interfaces;

namespace MediatrPatternFrom0.Services;

public class HelloCommand2: IRequest<string>
{
    
}
public class HelloComandHandler2 : IRequestHandler<HelloCommand2, string>
{
    public Task<string> Handler(HelloCommand2 request)
    {
       return Task.FromResult("hello");
    }
}

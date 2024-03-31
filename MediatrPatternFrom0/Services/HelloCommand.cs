using Pattern.Interfaces;

namespace MediatrPatternFrom0.Services;

public class HelloCommand : IRequest<string>
{
    public string Text { get; set; }    

}

public class HelloComandHandler : IRequestHandler<HelloCommand, string>
{

    public Task<string> Handler(HelloCommand request)
    {
        Console.WriteLine(request.Text);
        return Task.FromResult("Hello");
    }
}

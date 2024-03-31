
namespace MediatrPatternFrom0.RepositoryPattern;

public class Repository : IRepository
{
    public Task<string> Handle()
    {
        return Task.FromResult("hello");
    }
}

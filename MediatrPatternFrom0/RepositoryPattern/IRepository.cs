namespace MediatrPatternFrom0.RepositoryPattern;

public interface IRepository
{
    Task<string> Handle();
}

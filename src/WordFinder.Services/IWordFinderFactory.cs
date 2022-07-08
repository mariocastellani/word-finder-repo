namespace WordFinder.Services
{
    public interface IWordFinderFactory
    {
        IWordFinder CreateWordFinder(IEnumerable<string> matrix);
    }
}
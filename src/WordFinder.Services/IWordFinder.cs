namespace WordFinder.Services
{
    /// <summary>
    /// Allows you to implement a word finder with a specific algorithm.
    /// </summary>
    public interface IWordFinder
    {
        IEnumerable<string> Find(IEnumerable<string> wordStream);
    }
}
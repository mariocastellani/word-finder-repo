namespace WordFinder.Services
{
    public class WordFinderLinqFactory : IWordFinderFactory
    {
        public IWordFinder CreateWordFinder(IEnumerable<string> matrix)
        {
            return new WordFinderLinq(matrix);
        }
    }
}
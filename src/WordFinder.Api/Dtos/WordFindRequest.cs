namespace WordFinder.Api.Dtos
{
    public class WordFindRequest
    {
        public List<string> WordsToFind { get; set; }

        public List<string> SourceMatrix { get; set; }
    }
}
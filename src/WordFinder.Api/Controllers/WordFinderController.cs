using Microsoft.AspNetCore.Mvc;
using WordFinder.Api.Dtos;
using WordFinder.Services;

namespace WordFinder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordFinderController : ControllerBase
    {
        private readonly IWordFinderFactory _wordFinderFactory;

        public WordFinderController(IWordFinderFactory wordFinderFactory)
        {
            _wordFinderFactory = wordFinderFactory;
        }

        [HttpPost("Find")]
        public IEnumerable<string> Find(WordFindRequest request)
        {
            return _wordFinderFactory
                .CreateWordFinder(request.SourceMatrix)
                .Find(request.WordsToFind);
        }
    }
}
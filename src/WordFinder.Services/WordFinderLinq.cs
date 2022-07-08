using System.Text.RegularExpressions;

namespace WordFinder.Services
{
    /// <summary>
    /// <see cref="IWordFinder"/> implementation based on Linq.
    /// </summary>
    public class WordFinderLinq : IWordFinder
    {
        private const int MAX_SIZE = 64;

        private readonly string[] _matrix;
        private readonly string[] _transposed;

        public WordFinderLinq(IEnumerable<string> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            // Validate that the matrix does not exceed the maximum size
            if (matrix.Count() > MAX_SIZE)
                throw new ArgumentException("The length of the array must be less than 64.", nameof(matrix));

            // Validate that matrix width and heigth is equals
            if (matrix.Count() != (matrix.FirstOrDefault()?.Length ?? 0))
                throw new ArgumentException("The matrix must be square.", nameof(matrix));

            // Validate that all elements have the same length
            if (matrix.GroupBy(x => x.Length).Count() > 1)
                throw new ArgumentException("Each array element must have the same length.", nameof(matrix));

            _matrix = matrix.ToArray();

            // Transpose the source matrix to search vertically
            _transposed = matrix
                .Transpose()
                .Select(x => new string(x.ToArray()))
                .ToArray();
        }

        public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            if (wordStream == null || !_matrix.Any())
                return Enumerable.Empty<string>();

            // Here we take advantage of Linq and async
            // (each word is searched in async way)
            var tasks = wordStream
                .Distinct()
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(async x =>
                {
                    (string Word, int Count) record = (x, await FindWordAsync(x));
                    return record;
                })
                .ToArray();

            // Here we wait for all tasks to be completed
            var result = Task.WhenAll(tasks)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult(); 
            
            // Return just the TOP 10
            return result 
                .Where(x => x.Count > 0)
                .OrderByDescending(x => x.Count).ThenBy(x => x.Word)
                .Take(10)
                .Select(x => x.Word)
                .ToArray();
        }

        private Task<int> FindWordAsync(string word)
        {
            int count = 0;

            // Count occurrences horizontally
            count += _matrix.Aggregate(0, (acc, row) =>
                acc += Regex.Matches(row, $"({word})").Count);

            // Count occurrences vertically
            count += _transposed.Aggregate(0, (acc, row) =>
                acc += Regex.Matches(row, $"({word})").Count);

            return Task.FromResult(count);
        }
    }
}
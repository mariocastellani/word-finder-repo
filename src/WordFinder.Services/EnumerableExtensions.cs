namespace WordFinder.Services
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Swaps the rows and columns of a nested sequence.
        /// </summary>
        /// <typeparam name="T">The type of items in the sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>A sequence whose rows and columns are swapped.</returns>
        public static IEnumerable<IEnumerable<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> source)
        {
            return source
                .SelectMany(row => row.Select((item, i) => new KeyValuePair<int, T>(i, item)))
                .GroupBy(x => x.Key, y => y.Value)
                .Select(x => x as IEnumerable<T>);
        }
    }
}
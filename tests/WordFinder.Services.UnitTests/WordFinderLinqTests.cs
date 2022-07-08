using System;
using System.Collections.Generic;
using Xunit;

namespace WordFinder.Services.UnitTests
{
    public class WordFinderLinqTests
    {
        [Fact]
        public void NullMatrix_Error()
        {
            Assert.Throws<ArgumentNullException>(() => new WordFinderLinq(null));
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetMalformedMatrixes), MemberType = typeof(TestDataGenerator))]
        public void MalformedMatrixes_Errors(IEnumerable<string> matrix)
        {
            var exception = Assert.Throws<ArgumentException>(() => new WordFinderLinq(matrix));

            Assert.Contains(exception.Message, new[] 
            {
                "The length of the array must be less than 64. (Parameter 'matrix')",
                "The matrix must be square. (Parameter 'matrix')",
                "Each array element must have the same length. (Parameter 'matrix')"
            });
        }

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void FindMethod_Ok(IEnumerable<string> matrix, IEnumerable<string> wordStream, IEnumerable<string> expectedResult)
        {
            var finder = new WordFinderLinq(matrix);

            var result = finder.Find(wordStream);

            Assert.Equal(result, expectedResult);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WordFinder.Services.UnitTests
{
    /// <summary>
    /// Class useful for generating input data in theoretical and in fact tests.
    /// </summary>
    public class TestDataGenerator : TheoryData<IEnumerable<string>, IEnumerable<string>, IEnumerable<string>>
    {
        #region Malformed Matrixes

        public static IEnumerable<object[]> GetMalformedMatrixes()
        {
            // Matrix exceeding maximum width
            yield return new object[]
            {
                new string[] { new string(Enumerable.Repeat('x', 65).ToArray()) }
            };

            // Matrix exceeding max heigh
            yield return new object[]
            {
                Enumerable.Repeat("x", 65)
            };

            // Matrix with mismatched rows 
            yield return new object[]
            {
                Enumerable.Repeat("xx", 2)
                    .Union(Enumerable.Repeat("xxx", 2))
                    .Union(Enumerable.Repeat("xxxx", 2))
            };

            // Non square matrix
            yield return new object[]
            {
                Enumerable.Repeat("xxx", 4)
            };
        }

        #endregion

        #region Valid Matrixes

        public static IEnumerable<string> GetSimpleMatrix()
        {
            // Occurrences in this matrix:
            //   (2) cold
            //   (1) chill, wind

            return new string[]
            {
                "coldc",
                "xxwxo",
                "chill",
                "xxnxd",
                "xxdxx"
            };
        }

        public static IEnumerable<string> GetComplexMatrix()
        {
            // Occurrences in this matrix:
            //   (6) pig
            //   (5) cold
            //   (4) chill, wind
            //   (2) bull, egg, fill
            //   (1) fell, medal, men, see, sit, shell

            return new string[]
            {
                "coldcxxxxxxxxxxxxxxx",
                "xxwxoxxxxxxxxfillxxx",
                "chillxxxxxxxxxxxxxxx",
                "xxnxdxxpxxxxpigxxxxx",
                "xxdxxxxixxxxxxxxxxxp",
                "xxxxcxeggxxxxxxxxxxi",
                "xxwxoxxxxxxxxxxxxxxg",
                "chillxxxxxxxmxxxxxxx",
                "xxnxdxxxxxxmedalxxxx",
                "xxdxxxxxxxxpnxxxxxxx",
                "xxxxcxxxxxpigsxxxxxx",
                "xxwxoxxxxxxgpigxxxxx",
                "chillxxxxxxxxtxxxxxx",
                "xxnxdxxxxseexxxxxxxx",
                "xxdxxxxxshellxxxxxxx",
                "xxxxcxxxxxxxxxxxxxxx",
                "xxwxoxxxxxxxfellxxxx",
                "chillxxxxxxxigxxxxxx",
                "xxnxdxxxxbullgxxxxxx",
                "xxdxxxxxxxbullxxxxxx"
            };
        }

        #endregion

        #region Word streams

        public static readonly string[] SimpleWordStream = { "chill", "cold", "mess", "chill" };

        public static readonly string[] ComplexWordStream =
        {
            "chill", "cold", "mess", "chill", "medal", "sit", "left", "fill", "fell", "fill",
            "head", "pig", "egg", "bull", "men", "man", "wind", "shell", "push", "see"
        };

        #endregion

        public TestDataGenerator()
        {
            Add(Enumerable.Empty<string>(), SimpleWordStream, Enumerable.Empty<string>());
            Add(GetSimpleMatrix(), SimpleWordStream, new string[] { "cold", "chill" });
            Add(GetComplexMatrix(), ComplexWordStream, new string[]
            {
                "pig", "cold", "chill", "wind", "bull", "egg", "fill", "fell", "medal", "men"
            });
        }
    }
}
using System.Linq;
using DiceCore.Logic.Combinations.Implementations;
using DiceCore.Logic.Combinations.Interfaces;
using DiceCore.Models.Extensions;
using Xunit;

namespace DiceCoreTests.Combinations
{
    public class MultipleMatcherTest
    {
        private readonly ICombinationMatcher _combination;

        public MultipleMatcherTest()
        {
            _combination = new MultiPickMatcher();
        }

        [Fact]
        public void AllFail()
        {
            var set = new[] { 1, 1 };
            var result = _combination.Apply(set.SelectDices().ToList());

            Assert.False(result.Success);

            set = new[] { 1, 5, 6 };
            result = _combination.Apply(set.SelectDices().ToList());
            Assert.False(result.Success);

            set = new[] { 1, 5, 6, 4 };
            result = _combination.Apply(set.SelectDices().ToList());
            Assert.False(result.Success);
        }

        [Fact]
        public void SuccessAllForTriple()
        {
            var set = new[] { 1, 1, 1 };
            var result = _combination.Apply(set.SelectDices().ToList());

            Assert.True(result.Success);
            Assert.Equal(100, result.Score);

            set = new[] { 2, 2, 2 };
            result = _combination.Apply(set.SelectDices().ToList());
            Assert.True(result.Success);
            Assert.Equal(20, result.Score);

            set = new[] { 5, 5, 5 };
            result = _combination.Apply(set.SelectDices().ToList());
            Assert.True(result.Success);
            Assert.Equal(50, result.Score);
        }

        [Fact]
        public void SuccessAllForQuadruple()
        {
            var set = new[] { 1, 1, 1, 1 };
            var result = _combination.Apply(set.SelectDices().ToList());

            Assert.True(result.Success);
            Assert.Equal(200, result.Score);

            set = new[] { 2, 2, 2, 2 };
            result = _combination.Apply(set.SelectDices().ToList());
            Assert.True(result.Success);
            Assert.Equal(40, result.Score);

            set = new[] { 5, 5, 5, 5 };
            result = _combination.Apply(set.SelectDices().ToList());
            Assert.True(result.Success);
            Assert.Equal(100, result.Score);
        }

        [Fact]
        public void SuccessAllForFive()
        {
            var set = new[] { 1, 1, 1, 1, 1 };
            var result = _combination.Apply(set.SelectDices().ToList());

            Assert.True(result.Success);
            Assert.Equal(1000, result.Score);

            set = new[] { 2, 2, 2, 2, 2  };
            result = _combination.Apply(set.SelectDices().ToList());
            Assert.True(result.Success);
            Assert.Equal(200, result.Score);

            set = new[] { 5, 5, 5, 5, 5 };
            result = _combination.Apply(set.SelectDices().ToList());
            Assert.True(result.Success);
            Assert.Equal(500, result.Score);
        }
    }
}
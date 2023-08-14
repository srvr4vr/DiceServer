using System.Linq;
using DiceCore.Logic.Combinations.Implementations;
using DiceCore.Logic.Combinations.Models;
using DiceCore.Models.Extensions;
using Xunit;

namespace DiceCoreTests.Combinations
{
    public class StraightTest
    {
        private readonly StraightMatcher _combination;

        public StraightTest()
        {
            _combination = new StraightMatcher();
        }

        [Fact]
        public void ShouldFail()
        {
            var set = new[] { 1, 1, 3, 5, 6 };
            var result = _combination.Apply(set.SelectDices().ToList());

            Assert.False(result.Success);

            set = new[] { 1, 2, 3, 4, 6 };
            result = _combination.Apply(set.SelectDices().ToList());
            Assert.False(result.Success);
        }

        [Fact]
        public void ShouldBeHighStraight()
        {
            var set = new[] { 2, 3, 4, 5, 6 };
            var result = _combination.Apply(set.SelectDices().ToList());

            Assert.True(result.Success);
            Assert.Equal(250, result.Score);
            Assert.Equal(CombinationType.HighStraight, result.Type);
        }

        [Fact]
        public void ShouldBeLowStraight()
        {
            var set = new[] { 1, 2, 3, 4, 5 };
            var result = _combination.Apply(set.SelectDices().ToList());

            Assert.True(result.Success);
            Assert.Equal(125, result.Score);
            Assert.Equal(CombinationType.LowStraight, result.Type);
        }
    }
}
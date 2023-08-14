using System.Linq;
using DiceCore.Logic.Combinations.Implementations;
using DiceCore.Models.Extensions;
using Xunit;

namespace DiceCoreTests.Combinations
{
    public class SimpleCombinationTest
    {
        private readonly SimplePickMatcher _combination;

        public SimpleCombinationTest()
        {
            _combination = new SimplePickMatcher();   
        }

        [Fact]
        public void NotValidBecauseHasWrongDice()
        {
            var set = new[] {1, 5, 6};
            var result = _combination.Apply(set.SelectDices().ToList());

            Assert.False(result.Success);
        }

        [Fact]
        public void ShouldBe15()
        {
            var set = new[] { 1, 5 };
            var result = _combination.Apply(set.SelectDices().ToList());

            Assert.True(result.Success);
            Assert.Equal(15, result.Score);
        }
    }
}
using System.Linq;
using DiceCore.Logic.Combinations.Implementations;
using DiceCore.Logic.Combinations.Models;
using DiceCore.Models.Extensions;
using Xunit;

namespace DiceCoreTests.Combinations
{
    public class DetectorValidationTest
    {
        private readonly CombinationDetector _detector;

        public DetectorValidationTest()
        {
            _detector = new CombinationDetector();
        }

        [Fact]
        public void ShouldFail()
        {
            var set = new[] {4, 3};
            var result = _detector.ValidateCombination(set.SelectDices().ToList());

            Assert.False(result.Success);
            Assert.Equal(0, result.Score);
            Assert.Equal(CombinationType.None, result.Type);
        }

        [Fact]
        public void ShouldMultiplePick()
        {
            var set = new[] { 1, 1, 1 };
            var result = _detector.ValidateCombination(set.SelectDices().ToList());

            Assert.True(result.Success);
            Assert.Equal(100, result.Score);
            Assert.Equal(CombinationType.Set, result.Type);

            set = new[] { 2, 2, 2, 2 };
            result = _detector.ValidateCombination(set.SelectDices().ToList());

            Assert.True(result.Success);
            Assert.Equal(40, result.Score);
            Assert.Equal(CombinationType.FourOfKind, result.Type);

            set = new[] { 5, 5, 5, 5, 5 };
            result = _detector.ValidateCombination(set.SelectDices().ToList());

            Assert.True(result.Success);
            Assert.Equal(500, result.Score);
            Assert.Equal(CombinationType.Flush, result.Type);
        }

        [Fact]
        public void ShouldBeStraightPick()
        {
            var set = new[] { 1, 2, 3, 4, 5 };
            var result = _detector.ValidateCombination(set.SelectDices().ToList());

            Assert.True(result.Success);
            Assert.Equal(125, result.Score);
            Assert.Equal(CombinationType.LowStraight, result.Type);

            set = new[] { 2, 3, 4, 5, 6 };
            result = _detector.ValidateCombination(set.SelectDices().ToList());

            Assert.True(result.Success);
            Assert.Equal(250, result.Score);
            Assert.Equal(CombinationType.HighStraight, result.Type);
        }
    }
}
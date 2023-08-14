using System.Collections.Generic;
using DiceCore.Logic.Combinations.Interfaces;
using DiceCore.Logic.Combinations.Models;
using DiceCore.Models;

namespace DiceCore.Logic.Combinations.Implementations
{
    public class CombinationDetector : ICombinationDetector
    {
        private readonly ICombinationMatcher[] _matchers;

        public CombinationDetector()
        {
            _matchers = new ICombinationMatcher[]
            {
                new StraightMatcher(),
                new MultiPickMatcher(),
            };
        }

        public CombinationResult ValidateCombination(IReadOnlyCollection<Dice> diceSet)
        {
            foreach (var matcher in _matchers)
            {
                var result = matcher.Apply(diceSet);

                if (result.Success)
                {
                    return result;
                }
            }

            return default;
        }
    }
}
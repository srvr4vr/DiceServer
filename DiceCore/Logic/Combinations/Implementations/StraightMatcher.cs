using System.Collections.Generic;
using System.Linq;
using DiceCore.Logic.Combinations.Interfaces;
using DiceCore.Logic.Combinations.Models;
using DiceCore.Models;
using DiceCore.Models.Extensions;

namespace DiceCore.Logic.Combinations.Implementations
{
    public class StraightMatcher : ICombinationMatcher
    {
        private readonly (int[] set, CombinationType type, int score) _lowStraight = 
            (new[] {1, 2, 3, 4, 5}, CombinationType.LowStraight, 125);

        private readonly (int[] set, CombinationType type, int score) _highStraight = 
            (new[] {2, 3, 4, 5, 6}, CombinationType.HighStraight, 250);

        public CombinationResult Apply(IReadOnlyCollection<Dice> diceSet)
        {
            if (diceSet.Count != 5) return default;

            if (diceSet.IsSetEqual(_highStraight.set))
                return new CombinationResult(true, _highStraight.score, _highStraight.type);

            return diceSet.IsSetEqual(_lowStraight.set) 
                ? new CombinationResult(true, _lowStraight.score, _lowStraight.type) 
                : default;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using DiceCore.Logic.Combinations.Interfaces;
using DiceCore.Logic.Combinations.Models;
using DiceCore.Models;
using DiceCore.Models.Extensions;

namespace DiceCore.Logic.Combinations.Implementations
{
    public class SimplePickMatcher : ICombinationMatcher
    {
        public CombinationResult Apply(IReadOnlyCollection<Dice> diceSet) =>
            IsValid(diceSet)
                ? new CombinationResult(true, GetScore(diceSet), CombinationType.Simple)
                : default;

        private static bool IsValid(IEnumerable<Dice> diceSet) => 
            diceSet.All(dice => dice.Value == 10 || dice.Value == 5);

        private static int GetScore(IEnumerable<Dice> diceSet) => diceSet.SelectValues().Sum();
    }
}
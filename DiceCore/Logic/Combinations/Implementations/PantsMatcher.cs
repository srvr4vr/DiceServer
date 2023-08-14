using System.Collections.Generic;
using System.Linq;
using DiceCore.Logic.Combinations.Interfaces;
using DiceCore.Logic.Combinations.Models;
using DiceCore.Models;

namespace DiceCore.Logic.Combinations.Implementations
{
    public class PantsMatcher : ICombinationMatcher
    {
        public CombinationResult Apply(IReadOnlyCollection<Dice> diceSet) =>
            IsValid(diceSet) 
                ? new CombinationResult(true, 0, CombinationType.Pants)
                : default;

        private static bool IsValid(IReadOnlyCollection<Dice> diceSet) => 
            diceSet.Distinct().Count() == 1 && 
            diceSet.Count == 2;
    }
}
using System.Collections.Generic;
using DiceCore.Logic.Combinations.Models;
using DiceCore.Models;

namespace DiceCore.Logic.Combinations.Interfaces
{
    public interface ICombinationMatcher
    {
        CombinationResult Apply(IReadOnlyCollection<Dice> diceSet);
    }
}
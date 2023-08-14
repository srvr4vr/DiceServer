using System.Collections.Generic;
using System.Linq;

namespace DiceCore.Models.Extensions
{
    public static class DiceExtension
    {
        public static IEnumerable<int> SelectValues(this IEnumerable<Dice> dices) =>
            dices.Select(dice => dice.Value);

        public static IEnumerable<Dice> SelectDices(this IEnumerable<int> diceValues) =>
            diceValues.Select(value => new Dice(value));
        
        public static bool IsSetEqual(this IEnumerable<Dice> dices, IEnumerable<int> other) =>
            dices.Select(dice => dice.RawValue).SequenceEqual(other);
    }
}
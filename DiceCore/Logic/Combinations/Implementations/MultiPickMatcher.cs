using System.Collections.Generic;
using System.Linq;
using DiceCore.Logic.Combinations.Interfaces;
using DiceCore.Logic.Combinations.Models;
using DiceCore.Models;

namespace DiceCore.Logic.Combinations.Implementations
{
    public class MultiPickMatcher : ICombinationMatcher
    {
        private const int SetFactor = 10;
        private const int FourOfKindFactor = 20;
        private const int FlushFactor = 100;

        public CombinationResult Apply(IReadOnlyCollection<Dice> diceSet)
        {
            var distinct = diceSet
                .Distinct()
                .ToArray();

            var success = diceSet.Count > 2 && 
                          diceSet.Count <= GlobalConstants.DicePoolSize && 
                          distinct.Length == 1;

            return diceSet.Count switch
            {
                _ when !success => default,
                3 => new CombinationResult(true, SetFactor * distinct.First().Value, CombinationType.Set),
                4 => new CombinationResult(true, FourOfKindFactor * distinct.First().Value, CombinationType.FourOfKind),
                5 => new CombinationResult(true, FlushFactor * distinct.First().Value, CombinationType.Flush),
                _ => default
            };
        }
    }
}
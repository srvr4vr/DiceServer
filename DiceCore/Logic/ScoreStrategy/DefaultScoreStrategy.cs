using DiceCore.Logic.ScoreStrategy.Rules;
using DiceCore.Models;

namespace DiceCore.Logic.ScoreStrategy
{
    public class DefaultScoreStrategy : IScoreStrategy
    {
        private readonly IEndTurnRule[] _rules;

        public DefaultScoreStrategy()
        {
            _rules = new IEndTurnRule[]
            {
                new StartThresholdRule(GlobalConstants.WinScore, GlobalConstants.StartThreshold),
                new ScoreAsSameRule(GlobalConstants.WinScore),
                new DumpTruckRule(GlobalConstants.DumpTruckValue),
                new BarrelRule(GlobalConstants.BarrelThreshold),
                new BoltRule(GlobalConstants.WinScore, GlobalConstants.BarrelThreshold),
                new DefaultRule(GlobalConstants.WinScore)
            };
        }

        public TurnResult PerformRound(GameState gameState)
        {
            foreach (var endTurnRule in _rules)
            {
                if (endTurnRule.IsTriggered(gameState))
                {
                    return endTurnRule.Apply(gameState);
                }
            }

            return TurnResult.Done;
        }
    }
}
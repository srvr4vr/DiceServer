using DiceCore.Logic.ScoreStrategy;
using DiceCore.Logic.ScoreStrategy.Rules;
using DiceCore.Models;

namespace DiceCoreTests.Mocks
{
    public class TestScoreStrategy : IScoreStrategy
    {
        public TestScoreStrategy(int winScore)
        {
            _simpleRule = new DefaultRule(winScore);
        }

        private readonly IEndTurnRule _simpleRule;

        public TurnResult PerformRound(GameState gameState)
        {
            return _simpleRule.Apply(gameState);
        }
    }
}
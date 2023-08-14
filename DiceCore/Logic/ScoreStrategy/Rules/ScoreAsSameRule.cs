using DiceCore.Models;

namespace DiceCore.Logic.ScoreStrategy.Rules
{
    public class ScoreAsSameRule : DefaultRule
    {
        public ScoreAsSameRule(int winScore) : base(winScore)
        {
        }

        public override bool IsTriggered(GameState gameState) =>
            gameState.NextPlayer.Score ==
            gameState.CurrentPlayer.Score +
            gameState.CurrentPlayer.RoundScore;

        public override TurnResult Apply(GameState gameState)
        {
            gameState.NextPlayer.HardReset();

            base.Apply(gameState);

            return TurnResult.ScoreAsSame;
        }
    }
}
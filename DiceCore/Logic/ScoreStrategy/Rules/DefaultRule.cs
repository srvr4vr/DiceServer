using DiceCore.Models;

namespace DiceCore.Logic.ScoreStrategy.Rules
{
    public class DefaultRule : IEndTurnRule
    {
        protected readonly int WinScore;

        public DefaultRule(int winScore)
        {
            WinScore = winScore;
        }

        public virtual bool IsTriggered(GameState gameState) => true;

        public virtual TurnResult Apply(GameState gameState)
        {
            gameState.CurrentPlayer.AddRoundScore();
            gameState.CurrentPlayer.ResetRound();

            return gameState.CurrentPlayer.Score >= WinScore
                ? TurnResult.Win
                : TurnResult.Done;
        }
    }
}
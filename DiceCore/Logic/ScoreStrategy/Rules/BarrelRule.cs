using DiceCore.Models;

namespace DiceCore.Logic.ScoreStrategy.Rules
{
    public class BarrelRule : IEndTurnRule
    {
        private readonly int _threshold;

        public BarrelRule(int threshold)
        {
            _threshold = threshold;
        }

        public bool IsTriggered(GameState gameState) =>
            gameState.CurrentPlayer.Score < _threshold &&
            gameState.CurrentPlayer.Score + gameState.CurrentPlayer.RoundScore >= _threshold;

        public TurnResult Apply(GameState gameState)
        {
            gameState.CurrentPlayer.Score = _threshold;
            gameState.CurrentPlayer.ResetRound();
            return TurnResult.Barrel;
        }
    }
}
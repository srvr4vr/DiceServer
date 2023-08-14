using DiceCore.Models;

namespace DiceCore.Logic.ScoreStrategy.Rules
{
    public class StartThresholdRule : DefaultRule
    {
        private readonly int _startThreshold;

        public StartThresholdRule(int winScore, int threshold) : base(winScore)
        {
            _startThreshold = threshold;
        }

        public override bool IsTriggered(GameState gameState) => 
            gameState.CurrentPlayer.Score < _startThreshold;

        public override TurnResult Apply(GameState gameState)
        {
            if (gameState.CurrentPlayer.RoundScore >= _startThreshold)
            {
                return base.Apply(gameState);
            }

            gameState.CurrentPlayer.ResetRound();

            return TurnResult.Done;
        }
    }
}
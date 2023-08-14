using DiceCore.Models;

namespace DiceCore.Logic.ScoreStrategy.Rules
{
    public class BoltRule : DefaultRule
    {
        private readonly int _barrelThreshold;

        public BoltRule(int winScore, int barrelThreshold) : base(winScore)
        {
            _barrelThreshold = barrelThreshold;
        }

        public override bool IsTriggered(GameState gameState) =>
            gameState.CurrentPlayer.Score >= _barrelThreshold;

        public override TurnResult Apply(GameState gameState)
        {
            var player = gameState.CurrentPlayer;

            if (player.Score + player.RoundScore < WinScore)
            {
                player.BoltCount++;

                if (player.BoltCount < 3)
                    return TurnResult.Bolt;

                player.HardReset();

                return TurnResult.BoltZero;
            }

            player.BoltCount = 0;

            return base.Apply(gameState);
        }
    }
}
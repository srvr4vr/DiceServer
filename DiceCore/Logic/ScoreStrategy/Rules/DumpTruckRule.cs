using DiceCore.Models;

namespace DiceCore.Logic.ScoreStrategy.Rules
{
    public class DumpTruckRule : IEndTurnRule
    {
        private readonly int _value;

        public DumpTruckRule(int value)
        {
            _value = value;
        }

        public bool IsTriggered(GameState gameState) =>
            gameState.CurrentPlayer.Score + gameState.CurrentPlayer.RoundScore == _value;

        public TurnResult Apply(GameState gameState)
        {
            gameState.CurrentPlayer.HardReset();

            return TurnResult.DumpTruck;
        }
    }
}
using DiceCore.Models;

namespace DiceCore.Logic.ScoreStrategy
{
    public interface IScoreStrategy
    {
        TurnResult PerformRound(GameState gameState);
    }
}
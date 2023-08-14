using DiceCore.Models;

namespace DiceCore.Logic.ScoreStrategy.Rules
{
    public interface IEndTurnRule
    {
        bool IsTriggered(GameState gameState);
        TurnResult Apply(GameState gameState);
    }
}
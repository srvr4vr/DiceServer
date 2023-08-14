using System;
using System.Collections.Generic;
using DiceCore.Logic.ScoreStrategy;
using DiceCore.Models;

namespace DiceCore.Logic
{
    public interface IGame
    {
        event Action<IPlayer, IEnumerable<int>, GameState> DiceThrown;
        event Action<IPlayer, GameState, TurnResult> EndTurnEvent;

        void PerformCommand(IPlayer player, ActionType command, int[] diceIdx);
    }
}
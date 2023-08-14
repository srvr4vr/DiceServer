using System.Collections.Generic;
using DiceCore.Models;

namespace DiceCore
{
    public interface IPlayer
    {
        string Uid { get; }

        string Name { get; }

        int BoltCount { get; set; }

        int Score { get; set; }

        int RoundScore { get; set; }

        void AddRoundScore();

        PlayerDices PlayerDices { get; }
        void HardReset();

        void ResetRound();
    }
} 
using System.Collections.Generic;

namespace DiceCore
{
    public interface IPlayerBundle
    {
        IPlayer CurrentPlayer { get; }

        IPlayer NextPlayer { get; }

        IPlayer NextRound();

        IReadOnlyCollection<IPlayer> GetPlayers();

        void SetFirstPlayer(IPlayer player);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiceCore
{
    public class PlayerBundle : IPlayerBundle
    {
        private readonly PlayerBundleEnumerator _enumerator;

        public PlayerBundle(IEnumerable<IPlayer> players)
        {
            var playersList = players.ToList();

            if (playersList.Count < 2)
            {
                throw new ArgumentException($"Number of {nameof(players)} must be more than two");
            }

            _enumerator = new PlayerBundleEnumerator(playersList);
        }

        public IPlayer CurrentPlayer => _enumerator.Current;

        public IPlayer NextPlayer=> _enumerator.NextPlayer;

        public IPlayer NextRound()
        {
            _enumerator.MoveNext();
            return _enumerator.Current;
        }

        public IReadOnlyCollection<IPlayer> GetPlayers() => _enumerator.GetPlayers();

        public void SetFirstPlayer(IPlayer player) => _enumerator.SetFirstPlayer(player);
    }
}
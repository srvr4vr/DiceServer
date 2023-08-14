using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DiceCore
{
    public class PlayerBundleEnumerator : IEnumerator<IPlayer>
    {
        private readonly List<IPlayer> _players;
        private int _currentPlayerIndex;

        public PlayerBundleEnumerator(IEnumerable<IPlayer> players)
        {
            _players = players.ToList();
        }

        public bool MoveNext()
        {
            _currentPlayerIndex = GetNextPlayerIndex();
            return true;
        }

        public void Reset()
        {
            _currentPlayerIndex = 0;
        }

        public IPlayer Current => _players[_currentPlayerIndex];

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public IReadOnlyCollection<IPlayer> GetPlayers() => _players;

        public IPlayer NextPlayer => _players[GetNextPlayerIndex()];

        public void SetFirstPlayer(IPlayer player)
        {
            var index = _players.IndexOf(player);

            if (index < 0)
            {
                throw new ArgumentException($"Wrong {nameof(player)}!");
            }

            _currentPlayerIndex = index;
        }

        private int GetNextPlayerIndex() =>
            _currentPlayerIndex + 1 < _players.Count
                ? _currentPlayerIndex + 1
                : 0;
    }
}
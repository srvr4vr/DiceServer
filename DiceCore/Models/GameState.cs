using System.Collections.Generic;

namespace DiceCore.Models
{
    public class GameState
    {
        public GameStatus Status;

        private readonly IPlayerBundle _playersBundle;

        public GameState(params IPlayer[] players)
        {
            Status = GameStatus.InProgress;

            _playersBundle = new PlayerBundle(players);
        }

        public IPlayer NextRound() => _playersBundle.NextRound();

        public IPlayer NextPlayer => _playersBundle.NextPlayer;
        public IPlayer CurrentPlayer => _playersBundle.CurrentPlayer;

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
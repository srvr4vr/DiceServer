using System;

namespace DiceCore.Exceptions
{
    public class WrongPlayer : Exception
    {
        public IPlayer Player { get; }

        public WrongPlayer(IPlayer player)
        {
            Player = player;
        }

        public WrongPlayer(IPlayer player, string message): base(message)
        {
            Player = player;
        }

        public WrongPlayer(IPlayer player, string message, Exception inner)
            : base(message, inner)
        {
            Player = player;
        }
    }
}
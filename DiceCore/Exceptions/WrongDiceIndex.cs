using System;

namespace DiceCore.Exceptions
{
    public class WrongDiceIndex : Exception
    {
        public WrongDiceIndex()
        {
        }

        public WrongDiceIndex(string message)
            : base(message)
        {
        }

        public WrongDiceIndex(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
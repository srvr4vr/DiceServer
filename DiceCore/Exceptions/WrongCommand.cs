using System;

namespace DiceCore.Exceptions
{
    public class WrongCommand : Exception
    {
        public WrongCommand()
        {

        }

        public WrongCommand(string message)
            : base(message)
        {

        }

        public WrongCommand(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
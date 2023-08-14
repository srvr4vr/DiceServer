using System;

namespace DiceCore.Exceptions
{
    public class InactiveDicePick : Exception
    {
        public InactiveDicePick()
        {
            
        }

        public InactiveDicePick(string message)
            : base(message)
        {
            
        }

        public InactiveDicePick(string message, Exception inner)
            : base(message, inner)
        {
            
        }
    }
}
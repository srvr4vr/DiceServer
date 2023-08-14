using DiceCore.Logic;
using System.Collections.Generic;

namespace DiceServer.Logic
{
    public interface IGameFactory
    {
        IGame Create(IEnumerable<User> users);
    }
}
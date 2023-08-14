using System;

namespace DiceServer.Logic
{
    public interface IUserGameManager
    {
        bool IsUserInGame(User user, out Guid gameId);
        void PutUserInGame(User user, Guid gameId);
        bool Remove(User user);
    }
}
using DiceServer.Database.Models;
using System;

namespace DiceServer.Login.Interfaces
{
    public interface ITokenManager
    {
        Guid Create(User user);
        bool IsValid(Guid guid, string userId);
    }
}

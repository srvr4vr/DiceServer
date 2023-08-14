using DiceServer.Models;

namespace DiceServer.Login.Interfaces
{
    public interface IRegistrar
    {
        SuccessResponse Register(string login, string email, string password);
    }
}
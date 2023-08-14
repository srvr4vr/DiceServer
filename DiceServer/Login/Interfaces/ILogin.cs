using DiceServer.Models;

namespace DiceServer.Login.Interfaces
{
    public interface ILogin
    {
        HelloResponse Login(string login, string password);
    }
}
using DiceServer.Database.Models;

namespace DiceServer.Database
{
    public interface IUserRepository
    {
        User GetByName(string login);
        bool Add(string login, string email, string hash);
    }
}
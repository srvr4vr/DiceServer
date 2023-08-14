using System;
using System.Collections.Concurrent;
using DiceServer.Database.Models;

namespace DiceServer.Database
{
    public class UserRepository : IUserRepository
    {
        //ToDo: Заменить на реальную БД
        private readonly ConcurrentDictionary<string, User> _dict;

        public UserRepository()
        {
            _dict = new ConcurrentDictionary<string, User>();
            var user = new User()
            {
                Email = "srvr4vr@hotmail",
                Login = "srvr4vr",
                Guid = "c6c9c8c3-4565-483c-8a41-45b540a9fdf9",
                Hash = "qObDJKDVa0Kbrtf2AcWyL0cIXQR7rQq7ZvUW5IajnMc="
            };
            
            _dict.GetOrAdd("srvr4vr", user);
        }

        public User GetByName(string login)
        {
            if (_dict.TryGetValue(login, out var user))
            {
                return user;
            }

            return null;
        }

        public bool Add(string login, string email, string hash)
        {
            var user = new User()
            {
                Guid = Guid.NewGuid().ToString(),
                Email = email,
                Hash = hash,
                Login = login
            };
            return _dict.TryAdd(login, user);
        }
    }
}
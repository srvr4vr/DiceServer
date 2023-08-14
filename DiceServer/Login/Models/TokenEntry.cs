using DiceServer.Database.Models;
using System;

namespace DiceServer.Login.Models
{
    public class TokenEntry
    {
        public User User { get; }
        public Guid Guid { get; }
        public DateTime CreateDate { get; }

        public TokenEntry(User user)
        {
            User = user;
            Guid = Guid.NewGuid();
            CreateDate = DateTime.UtcNow;
        }
    }
}

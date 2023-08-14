using System;
using DiceServer.Database.Models;
using DiceServer.Login.Interfaces;
using DiceServer.Login.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DiceServer.Login.Implementations
{
    public class TokenManager : ITokenManager
    {
        private readonly IMemoryCache _cache;

        private readonly MemoryCacheEntryOptions _timeToLive = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

        public TokenManager(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Guid Create(User user)
        {
            var tokenEntry = new TokenEntry(user);

            _cache.Set(GetKey(tokenEntry.Guid), tokenEntry, _timeToLive);

            return tokenEntry.Guid;
        }

        public bool IsValid(Guid guid, string userId) => 
            _cache.Get(GetKey(guid)) is TokenEntry tokenEntry && 
            tokenEntry.User.Guid == userId;

        private static string GetKey(Guid guid) => 
            $"Token_{guid}";
    }
}
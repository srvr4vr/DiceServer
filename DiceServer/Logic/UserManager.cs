using DiceCore;
using DiceCore.Logic;
using DiceCore.Logic.ScoreStrategy;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiceServer.Logic
{
    public class User
    {
        public string Guid { get; set; }
        public string Name { get; set; }

        public string Foo; 

    }

    public class GameProcess
    {
        private readonly IGame _game;

        public GameProcess(IGame game)
        {
            _game = game;
        }
    }

    public class GameFactory : IGameFactory
    {
        private readonly IScoreStrategy _strategy;

        public GameFactory(IScoreStrategy strategy)
        {
            _strategy = strategy;
        }

        public IGame Create(IEnumerable<User> users) =>
            new Game(_strategy, users.Select(user => new Player(user.Name)).ToArray<IPlayer>());
    }

    public class GameManager
    {
        private readonly IGameFactory _gameFactory;
        private readonly IUserGameManager _userGameManager;
        private readonly ConcurrentDictionary<Guid, GameProcess> _games;

        public GameManager(IGameFactory gameFactory, IUserGameManager userGameManager)
        {
            _gameFactory = gameFactory;
            _userGameManager = userGameManager;
            _games = new ConcurrentDictionary<Guid, GameProcess>();
        }

        public Guid CreateGame(IEnumerable<User> users)
        {
            var game = _gameFactory.Create(users);
            var guid = Guid.NewGuid();
            var gameProcess = new GameProcess(game);

            foreach(var user in users)
            {
                _userGameManager.PutUserInGame(user, guid);
            }

            _games.TryAdd(guid, gameProcess);

            return guid;
        }

        public void CloseGame(Guid gameId)
        {
            _games.Remove(gameId, out _);

        }
    }
}

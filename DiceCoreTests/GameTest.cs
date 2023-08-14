using System;
using System.Linq;
using DiceCore;
using DiceCore.Exceptions;
using DiceCore.Logic;
using DiceCore.Models;
using DiceCoreTests.Mocks;
using Xunit;

namespace DiceCoreTests
{
    public class GameTest
    {
        private readonly IPlayer _playerOne;
        private readonly IPlayer _playerTwo;
        private readonly PlayerDices _playerOneDices;
        private readonly PlayerDices _playerTwoDices;
        private readonly Game _game;
        private readonly GameState _gameState;

        public GameTest()
        {
            _playerOneDices = new PlayerDices();
            _playerOne = new Player("one", _playerOneDices);

            _playerTwoDices = new PlayerDices();
            _playerTwo = new Player("two", _playerOneDices);

            _gameState = new GameState(_playerOne, _playerTwo);

            _game = new Game(new TestScoreStrategy(100), _gameState);
        }

        [Fact]
        public void WrongGameCommands()
        {
            _playerOneDices.SetDices(5, 4, 5, 3, 5);

            Assert.Throws<WrongCommand>(() => _game.PerformCommand(_playerOne, ActionType.Combo, 0, 3, 4));

            Assert.Throws<WrongPlayer>(() => _game.PerformCommand(_playerTwo, ActionType.Combo, 0, 3, 4));

            _game.PerformCommand(_playerOne, ActionType.Combo, 0,2,4);

            Assert.Throws<InactiveDicePick>(() => _game.PerformCommand(_playerOne, ActionType.Take, 0));

            Assert.Throws<WrongDiceIndex>(() => _game.PerformCommand(_playerOne, ActionType.Take, 8));
        }

        [Fact]
        public void EasyWinTest()
        {
            _playerOneDices.SetDices(5, 4, 5, 3, 5);

            _game.PerformCommand(_playerOne, ActionType.Combo, 0, 2, 4);

            _game.PerformCommand(_playerOne, ActionType.Ok);

            Assert.Equal(50, _playerOne.Score);

            _game.PerformCommand(_playerTwo, ActionType.Ok);

            Assert.Equal(0, _playerTwo.Score);

            _playerOneDices.SetDices(4, 4, 5, 1, 4);

            _game.PerformCommand(_playerOne, ActionType.Combo, 0, 1, 4);

            Assert.Equal(40, _playerOne.RoundScore);

            _game.PerformCommand(_playerOne, ActionType.Take, 2, 3);
            Assert.Equal(55, _playerOne.RoundScore);

            _game.PerformCommand(_playerOne, ActionType.Ok);

            Assert.Equal(105, _playerOne.Score);
            Assert.Equal(GameStatus.Ended, _gameState.Status);
        }

        [Fact]
        public void AllDiceTakeTest()
        {
            _playerOneDices.SetDices(1, 1, 5, 5, 5);
            _game.PerformCommand(_playerOne, ActionType.Take, 1,2,3,4);

            Assert.Equal(4, _playerOneDices.PickedDicesIdx.Count);
            Assert.True(_playerOneDices.PickedDicesIdx.SetEquals(new[] {  1, 2, 3, 4 }));
            Assert.Equal(25, _playerOne.RoundScore);

            _game.PerformCommand(_playerOne, ActionType.Take, 0);
            Assert.Equal(35, _playerOne.RoundScore);
            Assert.Empty(_playerOneDices.PickedDicesIdx);

            _game.PerformCommand(_playerOne, ActionType.Throw, 0, 1, 2, 3, 4);
        }
    }
}

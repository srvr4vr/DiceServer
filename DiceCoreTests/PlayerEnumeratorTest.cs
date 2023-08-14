using System;
using DiceCore;
using Moq;
using Xunit;

namespace DiceCoreTests
{
    public class PlayerEnumeratorTest
    {
        private readonly PlayerBundleEnumerator _enumerator;

        private readonly IPlayer _playerOne;
        private readonly IPlayer _playerTwo;

        public PlayerEnumeratorTest()
        {
            var playerOneMock = new Mock<IPlayer>();
            playerOneMock.Setup(x => x.Name).Returns("First");
            _playerOne = playerOneMock.Object;

            var playerTwoMock = new Mock<IPlayer>();
            playerTwoMock.Setup(x => x.Name).Returns("Second");

            _playerTwo = playerTwoMock.Object;

            _enumerator = new PlayerBundleEnumerator(new[] { _playerOne, _playerTwo });
        }

        [Fact]
        public void PlayerInitDefaultShouldBeFirst()
        {
            _enumerator.Reset();
            Assert.Equal("First", _enumerator.Current.Name);
        }

        [Fact]
        public void PlayerShouldBeFirst()
        {
            _enumerator.Reset();
            _enumerator.MoveNext();
            _enumerator.MoveNext();
            Assert.Equal("First", _enumerator.Current.Name);
        }

        [Fact]
        public void PlayerShouldBeSecond()
        {
            _enumerator.Reset();
            _enumerator.MoveNext();
            Assert.Equal("Second", _enumerator.Current.Name);
        }


        [Fact]
        public void SetupedPlayerShouldBeCurrent()
        {
            _enumerator.Reset();
            _enumerator.SetFirstPlayer(_playerTwo);
            Assert.Equal("Second", _enumerator.Current.Name);
        }

        [Fact]
        public void SecondSetupedPlayerShouldBeCurrent()
        {
            _enumerator.Reset();
            _enumerator.SetFirstPlayer(_playerTwo);
            _enumerator.SetFirstPlayer(_playerOne);

            Assert.Equal("First", _enumerator.Current.Name);
        }

        [Fact]
        public void ShouldFailWithWrongPlayer()
        {
            _enumerator.Reset();

            var playerThirdMock = new Mock<IPlayer>();
            playerThirdMock.Setup(x => x.Name).Returns("Third");

            Assert.Throws<ArgumentException>(() => _enumerator.SetFirstPlayer(playerThirdMock.Object));
        }
    }
}
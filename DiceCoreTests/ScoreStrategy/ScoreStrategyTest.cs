using DiceCore;
using DiceCore.Logic.ScoreStrategy;
using DiceCore.Models;
using Xunit;

namespace DiceCoreTests.ScoreStrategy
{
    public class ScoreStrategyTest
    {
        private readonly IScoreStrategy _scoreStrategy;
        private GameState State { get; set; }

        public ScoreStrategyTest()
        {
            _scoreStrategy = new DefaultScoreStrategy();

            var playerOne = new Player("one");
            var playerTwo = new Player("two");

            State = new GameState(new[] { playerOne, playerTwo });
        }

        [Fact]
        public void SimpleAdd()
        {
            State.CurrentPlayer.Score = 90;
            State.CurrentPlayer.RoundScore = 15;

            var result = _scoreStrategy.PerformRound(State);

            Assert.Equal(TurnResult.Done, result);
            Assert.Equal(105, State.CurrentPlayer.Score);
        }

        [Fact]
        public void StartThresholdFail()
        {
            State.CurrentPlayer.Score = 0;
            State.CurrentPlayer.RoundScore = 50;

            var result = _scoreStrategy.PerformRound(State);

            Assert.Equal(TurnResult.Done, result);
            Assert.Equal(0, State.CurrentPlayer.Score);
        }

        [Fact]
        public void StartThresholdOvercome()
        {
            State.CurrentPlayer.Score = 0;
            State.CurrentPlayer.RoundScore = 90;

            var result = _scoreStrategy.PerformRound(State);

            Assert.Equal(TurnResult.Done, result);
            Assert.Equal(90, State.CurrentPlayer.Score);
        }

        [Fact]
        public void ScoreAsSame()
        {
            State.NextPlayer.Score = 400;

            State.CurrentPlayer.Score = 390;
            State.CurrentPlayer.RoundScore = 10;

            var result = _scoreStrategy.PerformRound(State);

            Assert.Equal(TurnResult.ScoreAsSame, result);
            Assert.Equal(0, State.NextPlayer.Score);
        }

        [Fact]
        public void DumpTruck()
        {
            State.CurrentPlayer.Score = 500;
            State.CurrentPlayer.RoundScore = 55;

            var result = _scoreStrategy.PerformRound(State);

            Assert.Equal(TurnResult.DumpTruck, result);
            Assert.Equal(0, State.CurrentPlayer.Score);
        }


        [Fact]
        public void Barrel()
        {
            State.CurrentPlayer.Score = 900;
            State.CurrentPlayer.RoundScore = 55;

            var result = _scoreStrategy.PerformRound(State);

            Assert.Equal(TurnResult.Barrel, result);
            Assert.Equal(930, State.CurrentPlayer.Score);
        }

        [Fact]
        public void Bolt()
        {
            State.CurrentPlayer.Score = 930;
            State.CurrentPlayer.RoundScore = 55;

            var result = _scoreStrategy.PerformRound(State);

            Assert.Equal(TurnResult.Bolt, result);
            Assert.Equal(930, State.CurrentPlayer.Score);
            Assert.Equal(1, State.CurrentPlayer.BoltCount);

            State.CurrentPlayer.RoundScore = 14;

            result = _scoreStrategy.PerformRound(State);

            Assert.Equal(TurnResult.Bolt, result);
            Assert.Equal(930, State.CurrentPlayer.Score);
            Assert.Equal(2, State.CurrentPlayer.BoltCount);

            State.CurrentPlayer.RoundScore = 80;
            result = _scoreStrategy.PerformRound(State);

            Assert.Equal(TurnResult.Win, result);
            Assert.Equal(1010, State.CurrentPlayer.Score);
            Assert.Equal(0, State.CurrentPlayer.BoltCount);
        }

        [Fact]
        public void BoltZero()
        {
            State.CurrentPlayer.Score = 930;
            State.CurrentPlayer.RoundScore = 55;

            var result = _scoreStrategy.PerformRound(State);

            Assert.Equal(TurnResult.Bolt, result);
            Assert.Equal(930, State.CurrentPlayer.Score);
            Assert.Equal(1, State.CurrentPlayer.BoltCount);

            State.CurrentPlayer.RoundScore = 14;

            result = _scoreStrategy.PerformRound(State);

            Assert.Equal(TurnResult.Bolt, result);
            Assert.Equal(930, State.CurrentPlayer.Score);
            Assert.Equal(2, State.CurrentPlayer.BoltCount);

            result = _scoreStrategy.PerformRound(State);

            Assert.Equal(TurnResult.BoltZero, result);
            Assert.Equal(0, State.CurrentPlayer.Score);
            Assert.Equal(0, State.CurrentPlayer.BoltCount);
        }
    }
}
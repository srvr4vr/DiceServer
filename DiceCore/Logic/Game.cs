using System;
using System.Collections.Generic;
using DiceCore.Exceptions;
using DiceCore.Logic.Combinations.Implementations;
using DiceCore.Logic.Combinations.Models;
using DiceCore.Logic.ScoreStrategy;
using DiceCore.Models;

namespace DiceCore.Logic
{
    public class Game : IGame
    {
        private readonly GameState _gameState;

        public event Action<IPlayer, IEnumerable<int>, GameState> DiceThrown;
        public event Action<IPlayer, GameState, TurnResult> EndTurnEvent;

        private readonly IScoreStrategy _scoreStrategy;

        private readonly SimplePickMatcher _simpleCombinationMatcher;
        private readonly CombinationDetector _combinationDetector;
        private readonly PantsMatcher _pantsMatcher;

        public Game()
        {
            _simpleCombinationMatcher = new SimplePickMatcher();
            _pantsMatcher = new PantsMatcher();
            _combinationDetector = new CombinationDetector();
        }

        public Game(IScoreStrategy scoreStrategy, params IPlayer[] players) : this()
        {
            _scoreStrategy = scoreStrategy;
            _gameState = new GameState(players);
        }

        public Game(IScoreStrategy scoreStrategy, GameState gameState) : this()
        {
            _gameState = gameState;
            _scoreStrategy = scoreStrategy;
        }

        public Game(params IPlayer[] players) : this(new DefaultScoreStrategy(), players)
        {

        }

        public void PerformCommand(IPlayer player, ActionType command, params int[] diceIdx)
        {
            if (_gameState.Status == GameStatus.Ended)
            {
                throw  new WrongCommand();
            }

            if (_gameState.CurrentPlayer != player)
            {
                throw new WrongPlayer(player, "В очередь, в очередь, сукины дети!");
            }

            switch (command)
            {
                case ActionType.Combo:
                    PerformCombo(diceIdx);
                    break;
                case ActionType.Throw:
                    PerformThrow(diceIdx);
                    break;
                case ActionType.Take:
                    PerformTake(diceIdx);
                    break;
                case ActionType.Pants:
                    PerformPants(diceIdx);
                    break;
                case ActionType.Ok:
                    ProcessEndTurn();
                    break;
                case ActionType.Capitulate:
                    break;
                default:
                    break;
            }
        }

        private void PerformThrow(int[] diceIdx)
        {
            _gameState.CurrentPlayer.PlayerDices.ThrowDices(diceIdx);

            DiceThrown?.Invoke(_gameState.CurrentPlayer, diceIdx, _gameState);
        }

        private void PerformCombo(int[] diceIdx)
        {
            var dices = _gameState.CurrentPlayer.PlayerDices.GetDices(diceIdx);
            var result = _combinationDetector.ValidateCombination(dices);

            if (!result.Success)
            {
                throw new WrongCommand("Неверное комбо");
            }

            ConsumeTakeResult(result, diceIdx);
        }

        private void PerformPants(int[] diceIdx)
        {
            var dices = _gameState.CurrentPlayer.PlayerDices.GetDices(diceIdx);

            var pantsMatcherResult = _pantsMatcher.Apply(dices);

            if (pantsMatcherResult.Success)
            {
                PerformThrow(diceIdx);
            }
            else
            {
                throw new WrongCommand("Это вам не это!");
            }
        }

        private void PerformTake(int[] diceIdx)
        {
            var dices = _gameState.CurrentPlayer.PlayerDices.GetDices(diceIdx);
            var result = _simpleCombinationMatcher.Apply(dices);

            if (result.Success)
            {
                ConsumeTakeResult(result, diceIdx);
            }
            else
            {
                throw new WrongCommand("Не все кубики можно взять!");
            }
        }

        private void ConsumeTakeResult(CombinationResult result, int[] idx)
        {
            _gameState.CurrentPlayer.PlayerDices.TakeDices(idx);
            _gameState.CurrentPlayer.RoundScore += result.Score;
        }

        private void ProcessEndTurn()
        {
            var result = _scoreStrategy.PerformRound(_gameState);
            var player = _gameState.CurrentPlayer;

            if (result == TurnResult.Win)
            {
                _gameState.Status = GameStatus.Ended;
            }
            else
            {
                _gameState.NextRound();
            }

            EndTurnEvent?.Invoke(player, _gameState, result);
        }
    }
}
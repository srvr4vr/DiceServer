using System;
using System.Collections.Generic;
using System.Linq;
using DiceCore.Exceptions;
using DiceCore.Logic;
using DiceCore.Models;

namespace DiceCore
{
    public class PlayerDices
    {
        private Dice[] _dices = new Dice[GlobalConstants.DicePoolSize];

        private readonly IDiceGenerator _diceGenerator;
        public readonly HashSet<int> PickedDicesIdx;

        public PlayerDices()
        {
            _diceGenerator = new DiceGenerator(GlobalConstants.DiceBase);

            PickedDicesIdx = new HashSet<int>(GlobalConstants.DicePoolSize);

            for (var i = 0; i < _dices.Length; i++)
            {
                _dices[i] = new Dice();
            }
        }

        public IReadOnlyCollection<Dice> GetDices(params int[] diceIdx)
        {
            foreach (var i in diceIdx)
            {
                ThrowIfIndexesOutOfRange(i);
            }

            return diceIdx
                .Select(index => _dices[index])
                .ToArray();
        }

        public void SetDices(IEnumerable<Dice> dices)
        {
            var diceArray = dices.ToArray();

            if (diceArray.Length != GlobalConstants.DicePoolSize)
            {
                throw new ArgumentException("wrong dice count!");
            }

            _dices = diceArray;
        }

        /// <summary>
        /// Установить дайсы.
        /// </summary>
        /// <exception cref="ArgumentException">Неправильное количество дайсов</exception>
        /// <param name="dices"></param>
        public void SetDices(params int[] dices)
        {
            if (dices.Length != GlobalConstants.DicePoolSize)
            {
                throw new ArgumentException("wrong dice count!");
            }

            _dices = dices
                .Select(x=>new Dice(x))
                .ToArray();
        }

        /// <summary>
        ///  Взять кости со стола
        /// </summary>
        /// <exception cref="WrongDiceIndex">Попытка задействовать дайс за пределом доступных индексов</exception>
        /// <exception cref="InactiveDicePick">Попытка задействовать неактивный дайс</exception>
        /// <param name="idx">Индексы дайсов</param>
        public void TakeDices(params int[] idx)
        {
            CheckDicesAndTrowIfNeeded(idx);

            foreach (var i in idx)
            {
                PickedDicesIdx.Add(i);
            }

            if (PickedDicesIdx.Count >= GlobalConstants.DicePoolSize)
            {
                PickedDicesIdx.Clear();
            }
        }

        /// <summary>
        /// Кинуть дайсы
        /// </summary>
        /// <exception cref="WrongDiceIndex">Попытка кинуть дайс за пределом доступных индексов</exception>
        /// <exception cref="InactiveDicePick">Попытка кинуть неактивный дайс</exception>
        /// <param name="idx">Индексы дайсов</param>
        public void ThrowDices(params int[] idx)
        {
            CheckDicesAndTrowIfNeeded(idx);

            for (var i = 0; i < _dices.Length; i++)
            {
                var dice = _diceGenerator.GenerateDice();
                _dices[i] = dice;
            }
        }

        /// <summary>
        ///     Проверка корректности индексов дайсов
        /// </summary>
        /// <exception cref="WrongDiceIndex">Попытка задействовать дайс за пределом доступных индексов</exception>
        /// <exception cref="InactiveDicePick">Попытка задействовать неактивный дайс</exception>
        /// <param name="idx">Индексы дайсов</param>
        private void CheckDicesAndTrowIfNeeded(IEnumerable<int> idx)
        {
            foreach (var i in idx)
            {
                ThrowIfIndexesOutOfRange(i);

                if (PickedDicesIdx.Contains(i))
                {
                    throw new InactiveDicePick($"Dice with {i} index is not available");
                }
            }
        }

        private static void ThrowIfIndexesOutOfRange(int i)
        {
            if (i >= GlobalConstants.DicePoolSize || i < 0)
            {
                throw new WrongDiceIndex($"Dice Index {i} out of possible range");
            }
        }
    }
}
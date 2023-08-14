using System;
using DiceCore.Models;

namespace DiceCore.Logic
{
    public class DiceGenerator : IDiceGenerator
    {
        private readonly int _diceBase;
        private readonly Random _random;

        public DiceGenerator(int diceBase)
        {
            _diceBase = diceBase;
            _random = new Random();
        }

        public Dice GenerateDice() => new Dice(_random.Next(1, _diceBase));
    }
}
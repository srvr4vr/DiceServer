namespace DiceCore.Models
{
    public struct Dice
    {
        public int RawValue { get; }

        public int Value => RawValue == 1 ? 10 : RawValue;

        public Dice(int value)
        {
            RawValue = value;
        }

        public override string ToString() => Value.ToString();
    }
}
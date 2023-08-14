namespace DiceCore.Logic.Combinations.Models
{
    public struct CombinationResult
    {
        public bool Success { get; }
        public int Score { get; }
        public CombinationType Type { get; }

        public CombinationResult(bool success, int score, CombinationType type)
        {
            Success = success;
            Score = score;
            Type = type;
        }
    }
}
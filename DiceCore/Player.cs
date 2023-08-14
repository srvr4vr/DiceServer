namespace DiceCore
{
    public class Player : IPlayer
    {
        public string Uid { get; }

        public string Name { get; }
        public int BoltCount { get; set; }

        public int Score { get; set; }

        public int RoundScore { get; set; }

        public void AddRoundScore()
        {
            Score += RoundScore;
            RoundScore = 0;
        }

        public PlayerDices PlayerDices { get; }

        public Player(string name)
        {
            Name = name;
            PlayerDices = new PlayerDices();
        }

        public Player(string name, PlayerDices playerDices)
        {
            Name = name;
            PlayerDices = playerDices;
        }

        public void HardReset()
        {
            Score = 0;
            RoundScore = 0;
            BoltCount = 0;
            PlayerDices.PickedDicesIdx.Clear();
        }

        public void ResetRound()
        {
            PlayerDices.PickedDicesIdx.Clear();
            RoundScore = 0;
        }
    }
}
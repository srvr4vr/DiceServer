namespace DiceServer.Database.Models
{
    public class User
    {
        public string Guid { get; set; }
        public string Login { get; set; }
        public string Hash {get;set;}
        public string Email { get; set; }
    }
}
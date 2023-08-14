namespace DiceServer.Models
{
    public class HelloResponse : SuccessResponse
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string Address { get; set; }
    }
}
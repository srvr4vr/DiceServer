using Newtonsoft.Json;

namespace DiceServer.WebSockets.Models
{
    [JsonObject]
    public class SuccessResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
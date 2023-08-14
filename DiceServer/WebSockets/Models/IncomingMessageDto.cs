using Newtonsoft.Json;

namespace DiceServer.WebSockets.Models
{
    [JsonObject]
    public class IncomingMessageDto
    {
        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("modifier")]
        public string Modifier { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
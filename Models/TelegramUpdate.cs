using System.Text.Json.Serialization;

namespace TelegramBotServer.Models
{
    public class TelegramUpdate
    {
        [JsonPropertyName("message")]
        public TelegramMessage? Message { get; set; }
    }
}

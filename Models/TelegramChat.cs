using System.Text.Json.Serialization;

namespace TelegramBotServer.Models
{
    public class TelegramChat
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}

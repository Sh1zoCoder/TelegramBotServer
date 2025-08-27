using System.Text.Json.Serialization;

namespace TelegramBotServer.Models
{
    public class TelegramMessage
    {
        [JsonPropertyName("message_id")]
        public long MessageId { get; set; }

        [JsonPropertyName("chat")]
        public TelegramChat Chat { get; set; } = default!;

        [JsonPropertyName("from")]
        public TelegramUser From { get; set; } = default!;

        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }
}

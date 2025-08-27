using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using TelegramBotServer.Models;

namespace TelegramBotServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BotController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _botToken;

        public BotController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _botToken = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN")
                        ?? config["Telegram:BotToken"]
                        ?? throw new InvalidOperationException("Bot token is not configured.");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TelegramUpdate update)
        {
            // Страхуемся от пустых апдейтов
            var message = update?.Message;
            if (message == null || string.IsNullOrWhiteSpace(message.Text))
                return Ok(); // Ничего не отвечаем на не-текстовые апдейты

            var text = message.Text.Trim().ToLowerInvariant();
            string reply;

            if (text.Contains("привет"))
                reply = $"Привет, {message.From?.FirstName ?? "друг"}!";
            else if (text.Contains("кто ты"))
                reply = "Я ASP.NET Core бот!";
            else
                reply = "Я тебя не понимаю :(";

            var payload = new
            {
                chat_id = message.Chat.Id,
                text = reply
            };

            var client = _httpClientFactory.CreateClient();
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"https://api.telegram.org/bot{_botToken}/sendMessage";
            using var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            return Ok();
        }
    }
}

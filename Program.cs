using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Слушаем http://localhost:5000 (удобно под lt/ngrok)
builder.WebHost.UseUrls("http://localhost:5000");

// HttpClientFactory
builder.Services.AddHttpClient();

builder.Services
    .AddControllers()
    .AddJsonOptions(o =>
    {
        // НЕ используем SnakeCaseLower — его нет в стандартной библиотеке
        // На вход Telegram присылает snake_case, мы сопоставим это атрибутами [JsonPropertyName]
        o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

using ChineseStoryHome.Api.Services;
using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;
using System.Text.Json;
using ChineseStoryHome.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<IOpenAiService, OpenAiService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder.WithOrigins("https://localhost:7129", "https://chinesestorygenerator-begyd4gyceetesht.centralus-01.azurewebsites.net/")// Change to your Blazor app's URL
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
        if (app.Environment.IsDevelopment())
            options.RoutePrefix = "swagger";
        else
            options.RoutePrefix = string.Empty;
    }
);
app.UseSwagger();
app.UseCors("AllowLocalhost"); // Enable CORS

app.MapPost("/api/generate-title", async (IOpenAiService openAiService, [FromBody] string prompt) =>
{
    var title = await openAiService.GenerateTitleAsync(prompt);
    return Results.Ok(title);
});

app.MapPost("/api/generate-story", async (IOpenAiService openAiService, [FromBody] string json) =>
{
    Console.WriteLine(json);
    MyChatMessage myMessage = JsonSerializer.Deserialize<MyChatMessage>(json);
    ChatMessage[] prompt = new ChatMessage[]
    {
        new SystemChatMessage(myMessage.SystemMessage),
        new UserChatMessage(myMessage.UserMessage)
    };
    var story = await openAiService.GenerateStoryAsync(prompt);
    return Results.Ok(story);
});

app.Run();


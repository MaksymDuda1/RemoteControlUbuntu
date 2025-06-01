using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using RemoteControlUbuntu.Application.Abstractions.Services;

namespace RemoteControlUbuntu.Infrastructure.Services;

public class OpenAICaller(IConfiguration configuration) : IOpenAICaller
{
    public async Task CallOpenAI(string request)
    {
        var key = configuration["OpenAI:ApiKey"];
        ChatClient client = new(model: "gpt-4o-mini", apiKey: key);

        ChatCompletion completion = await client.CompleteChatAsync(request);
        
        Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");
    }
}
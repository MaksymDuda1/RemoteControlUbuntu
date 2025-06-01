using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Infrastructure.OpenAIIntegration;

namespace RemoteControlUbuntu.Application.Services;

public class OpenAIService(IOpenAICaller openAiCaller) : IOpenAIService
{
    public async Task<string?> AskChatGPT(string request)
    {
         return await openAiCaller.CallOpenAI(request);
    }
}
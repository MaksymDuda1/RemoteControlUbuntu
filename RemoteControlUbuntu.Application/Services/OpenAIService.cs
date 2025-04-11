using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Infrastructure.OpenAIIntegration;

namespace RemoteControlUbuntu.Application.Services;

public class OpenAIService(IOpenAICaller openAiCaller) : IOpenAIService
{
    public async Task AskChatGPT(string request)
    {
        await openAiCaller.CallOpenAI(request);
    }
}
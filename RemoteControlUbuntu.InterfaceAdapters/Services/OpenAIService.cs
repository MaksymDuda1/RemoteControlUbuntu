using RemoteControlUbuntu.Application.Abstractions.Services;

namespace RemoteControlUbuntu.Infrastructure.Services;

public class OpenAiService(IOpenAICaller openAiCaller) : IOpenAIService
{
    public async Task AskChatGPT(string request)
    {
        await openAiCaller.CallOpenAI(request);
    }
}
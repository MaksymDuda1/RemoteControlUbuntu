namespace RemoteControlUbuntu.Infrastructure.OpenAIIntegration;

public interface IOpenAICaller
{
    Task<string?> CallOpenAI(string request);
}
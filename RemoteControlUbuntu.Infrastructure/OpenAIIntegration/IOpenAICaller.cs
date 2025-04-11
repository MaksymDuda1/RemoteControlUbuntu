namespace RemoteControlUbuntu.Infrastructure.OpenAIIntegration;

public interface IOpenAICaller
{
    Task CallOpenAI(string request);
}
namespace RemoteControlUbuntu.Application.Abstractions;

public interface IOpenAIService
{ 
    Task<string?> AskChatGPT(string request);
}
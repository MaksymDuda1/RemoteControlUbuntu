namespace RemoteControlUbuntu.Application.Abstractions.Services;

public interface IOpenAIService
{ 
    Task AskChatGPT(string request);
}
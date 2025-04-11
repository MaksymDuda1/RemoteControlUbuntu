namespace RemoteControlUbuntu.Application.Abstractions;

public interface IOpenAIService
{ 
    Task AskChatGPT(string request);
}
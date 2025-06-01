namespace RemoteControlUbuntu.Application.Abstractions.Services;

public interface IOpenAICaller
{
    Task CallOpenAI(string request);
}
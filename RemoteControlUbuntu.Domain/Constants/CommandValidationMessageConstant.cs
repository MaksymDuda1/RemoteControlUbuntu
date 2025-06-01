namespace RemoteControlUbuntu.Domain.Constants;

public class CommandValidationMessageConstant
{
    public const string BlackListedCommandMessage = "Command is related to blacklist and cannot be created";
    
    public const string WhiteListedCommandMessage = "Command is related to whitelist and we garantue its successful execution";
    
    public const string CustomCommandMessage = "Command is custom and we cannot garantue its successful execution";
}
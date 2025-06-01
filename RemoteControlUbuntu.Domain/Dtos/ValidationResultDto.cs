using RemoteControlUbuntu.Domain.Enums;

namespace RemoteControlUbuntu.Domain.Dtos;

public class ValidationResultDto
{
    public CommandValidationResult CommandValidationResult { get; set; }

    public string Message { get; set; }
}
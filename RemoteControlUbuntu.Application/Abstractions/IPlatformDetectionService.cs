using RemoteControlUbuntu.Domain.Entities;
using RemoteControlUbuntu.Domain.Enums;

namespace RemoteControlUbuntu.Application.Abstractions;

public interface IPlatformDetectionService
{
    Task<PlatformType> DetectPlatformAsync(Connection connection);
    Task<bool> IsPlatformSupportedAsync(PlatformType platform);
    Task<Dictionary<string, string>> GetPlatformCommandMappingsAsync(PlatformType sourcePlatform, PlatformType targetPlatform);
}
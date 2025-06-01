using System.Security.Claims;
using RemoteControlUbuntu.Application.Models;
using RemoteControlUbuntu.Infrastructure.Entities;

namespace RemoteControlUbuntu.Infrastructure.Abstractions.Services;

public interface ITokenService
{
    Task<string> RefreshToken(TokenApiModel token);
    Task RevokeTokenAsync(ClaimsPrincipal principal);
    string CreateAccessToken(ClaimsIdentity claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredTokenAsync(string token);
    Task<ClaimsIdentity> GenerateClaims(User user);
}
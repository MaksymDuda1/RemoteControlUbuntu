using System.Security.Claims;
using RemoteControlUbuntu.Application.Models;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Application.Abstractions;

public interface ITokenService
{
    Task<string> RefreshToken(TokenApiModel token);
    Task RevokeTokenAsync(ClaimsPrincipal principal);
    string CreateAccessToken(ClaimsIdentity claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredTokenAsync(string token);
    Task<ClaimsIdentity> GenerateClaims(User user);
}
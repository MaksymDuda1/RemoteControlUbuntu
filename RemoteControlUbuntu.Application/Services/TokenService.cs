using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Application.Exceptions;
using RemoteControlUbuntu.Application.Models;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Application.Services;

public class TokenService : ITokenService
{
    private readonly UserManager<User> userManager;
    private readonly IConfiguration configuration;
    private readonly string jwtSecret;

    public TokenService(
        UserManager<User> userManager,
        
        IConfiguration configuration)
    {
        this.userManager = userManager;
        this.configuration = configuration;
        jwtSecret = this.configuration["JWT:Secret"];
    }

    public async Task<string> RefreshToken(TokenApiModel token)
    {
        try
        {
            var principal = GetPrincipalFromExpiredTokenAsync(token.AccessToken);
            string username = principal.Identity.Name!;
            var user = await userManager.FindByNameAsync(username);

            if (user is null)
                throw new EntityNotFoundException("User Not Found");


            if (user.RefreshToken != token.RefreshToken)
            {
                throw new CredentialValidationException("Refresh token is not recognised.");
            }


            if (user.RefreshTokenExpiration <= DateTime.Now)
            {
                throw new ValidationException("Refresh token is not valid due to expiration");
            }

            var claims = await GenerateClaims(user);
            var accessToken = CreateAccessToken(claims);

            return accessToken;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task RevokeTokenAsync(ClaimsPrincipal principal)
    {
        var userEmail = principal.Identity.Name;
        var user = await userManager.FindByEmailAsync(userEmail);
        if (user == null) throw new Exception("Invalid client request");
        user.RefreshToken = null;
        await userManager.UpdateAsync(user);
    }

    public string CreateAccessToken(ClaimsIdentity claims)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();

            var privateKey = Encoding.ASCII.GetBytes(jwtSecret);

            var credentials =
                new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(Convert.ToInt16(configuration["JWT:AccessTokenExpirationHours"])),
                Subject = claims,
                Issuer = configuration["JWT:ValidIssuer"],
                Audience = configuration["JWT:ValidAudience"]
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
        catch (Exception ex)
        {
            throw new AuthenticationException();
        }
    }

    public string GenerateRefreshToken()
    {
        try
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        catch
        {
            throw new AuthenticationException();
        }
    }

    public ClaimsPrincipal GetPrincipalFromExpiredTokenAsync(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
        return principal;
    }

    public async Task<ClaimsIdentity> GenerateClaims(User user)
    {
        try
        {
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.Name, user.UserName!));
            claims.AddClaim(new Claim(ClaimTypes.Email, user.Email!));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
                claims.AddClaim(new Claim(ClaimTypes.Role, role));

            return claims;
        }
        catch (Exception ex)
        {
            throw new AuthenticationException();
        }
    }
}
using System.Security.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Application.Exceptions;
using RemoteControlUbuntu.Application.Models;
using RemoteControlUbuntu.Domain.Dtos;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.Application.Services;

public class AuthorizationService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    ITokenService tokenService,
    IConfiguration configuration) : IAuthorizationService
{
    public async Task<TokenApiModel> Login(LoginDto loginDto)
    {
        var userByEmail = await userManager.FindByEmailAsync(loginDto.Email);

        if (userByEmail is null)
            throw new EntityNotFoundException("No user with given email was found");

        var result = await signInManager
            .PasswordSignInAsync(userByEmail.UserName, loginDto.Password, false, false);

        if (!result.Succeeded)
            throw new CredentialValidationException("Wrong password");

        var claims = await tokenService.GenerateClaims(userByEmail);

        var accessToken = tokenService.CreateAccessToken(claims);
        var refreshToken = tokenService.GenerateRefreshToken();

        userByEmail.RefreshToken = refreshToken;
        userByEmail.RefreshTokenExpiration =
            DateTime.UtcNow.AddDays(Convert.ToInt16(configuration["JWT:RefreshTokenExpirationsDays"]));

        await userManager.UpdateAsync(userByEmail);

        return new TokenApiModel()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };
    }

    public async Task<IdentityResult> Registration(RegistrationDto registrationDto)
    {
        var user = new User()
        {
            Email = registrationDto.Email,
            UserName = registrationDto.Username,
        };

        var result = await userManager.CreateAsync(user, registrationDto.Password);

        if (!result.Succeeded)
            throw new AuthenticationException("Wrong data");

        await userManager.AddToRoleAsync(user, "User");
        await userManager.UpdateAsync(user);

        return result;
    }
    
    public Task Logout()
    {
        return signInManager.SignOutAsync();
    }
}
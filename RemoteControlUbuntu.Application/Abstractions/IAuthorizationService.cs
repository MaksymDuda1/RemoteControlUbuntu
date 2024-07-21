using Microsoft.AspNetCore.Identity;
using RemoteControlUbuntu.Application.Models;
using RemoteControlUbuntu.Domain.Dtos;

namespace RemoteControlUbuntu.Application.Abstractions;

public interface IAuthorizationService
{
    Task<TokenApiModel> Login(LoginDto loginDto);
    Task<IdentityResult> Registration(RegistrationDto registrationDto);
    Task Logout();
}
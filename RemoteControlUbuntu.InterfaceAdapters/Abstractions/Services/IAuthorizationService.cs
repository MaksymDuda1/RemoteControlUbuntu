using Microsoft.AspNetCore.Identity;
using RemoteControlUbuntu.Application.Models;
using RemoteControlUbuntu.Domain.DTOs;

namespace RemoteControlUbuntu.Infrastructure.Abstractions.Services;

public interface IAuthorizationService
{
    Task<TokenApiModel> Login(LoginDto loginDto);
    Task<IdentityResult> Registration(RegistrationDto registrationDto);
    Task Logout();
}
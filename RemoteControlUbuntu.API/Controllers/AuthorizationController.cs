using Microsoft.AspNetCore.Mvc;
using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Application.Models;
using RemoteControlUbuntu.Domain.Dtos;

namespace RemoteControlUbuntu.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthorizationController(IAuthorizationService authorizationService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<TokenApiModel>> Login(LoginDto request)
    {
        return Ok(await authorizationService.Login(request));
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Registration(RegistrationDto request)
    {
        return Ok(await authorizationService.Registration(request));
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await authorizationService.Logout();
        return NoContent();
    }
}
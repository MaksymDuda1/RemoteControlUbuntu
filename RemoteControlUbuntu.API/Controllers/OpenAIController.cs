using Microsoft.AspNetCore.Mvc;
using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Application.Services;
using RemoteControlUbuntu.Domain.Dtos;
using RemoteControlUbuntu.Domain.Enums;

namespace RemoteControlUbuntu.API.Controllers;

[ApiController]
[Route("api/openAi")]
public class OpenAIController(IValidationService validationService) : ControllerBase
{
    [HttpPost("command")]
    public async Task<IActionResult> GetCommand([FromBody] GetCommandFromAIDto request, [FromQuery] PlatformType os)
    {
        var response = await validationService.GetCommandFromUserRequestAndCheckBlackListMatches(request.Request, os);
        
        return Ok(response);
    }
}
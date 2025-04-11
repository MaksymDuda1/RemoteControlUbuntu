using Microsoft.AspNetCore.Mvc;
using RemoteControlUbuntu.Application.Abstractions;

namespace RemoteControlUbuntu.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OpenAIController(IOpenAIService aiService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> AskChatGPR([FromQuery] string request)
    {
        await aiService.AskChatGPT(request);
        
        return Ok();
    }
}
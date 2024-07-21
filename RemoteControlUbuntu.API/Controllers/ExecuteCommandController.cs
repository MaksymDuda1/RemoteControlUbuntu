using Microsoft.AspNetCore.Mvc;
using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Domain.Dtos;

namespace RemoteControlUbuntu.API.Controllers;

[ApiController]
[Route("api/execute")]
public class ExecuteCommandController(IExecuteCommandService executeCommandService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Execute([FromBody] UserConnectionCommandDto request)
    {
        await executeCommandService.Execute(request);
        return Ok();
    }
}
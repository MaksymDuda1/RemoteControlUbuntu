using Microsoft.AspNetCore.Mvc;
using RemoteControlUbuntu.Application.Abstractions.Services;
using RemoteControlUbuntu.Domain.DTOs;

namespace RemoteControlUbuntu.Web.Controllers;

[ApiController]
[Route("api/execute")]
public class ExecuteCommandController(IExecuteCommandService executeCommandService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Execute([FromBody] ConnectionCommandDto request)
    {
        await executeCommandService.Execute(request);
        return Ok();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
    }
}
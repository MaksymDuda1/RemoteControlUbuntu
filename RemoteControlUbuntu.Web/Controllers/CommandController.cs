using Microsoft.AspNetCore.Mvc;
using RemoteControlUbuntu.Domain.DTOs;
using RemoteControlUbuntu.Infrastructure.Abstractions.Repositories;
using RemoteControlUbuntu.Infrastructure.Abstractions.Services;
using RemoteControlUbuntu.Infrastructure.DTOs;

namespace RemoteControlUbuntu.Web.Controllers;

[ApiController]
[Route("api/command")]
public class CommandController(
    ICommandService commandService,
    IUnitOfWork unitOfWork) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<CommandDto>>> GetAll()
    {
        return Ok(await unitOfWork.Commands.GetAllAsync());
    }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<List<CommandDto>>> GetAllCommands(Guid userId)
    {
        return Ok(await commandService.GetAllUserCommand(userId));
    }

    [HttpGet("by-id/{commandId:guid}")]
    public async Task<ActionResult<CommandDto>> GetCommandById(Guid commandId)
    {
        return Ok(await commandService.GetCommandById(commandId));
    }

    [HttpPost]
    public async Task<IActionResult> AddCommand([FromBody] AddCommandDto addCommandDto)
    {
        await commandService.AddCommand(addCommandDto);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCommand([FromBody] UpdateCommandDto updateCommandDto)
    {
        await commandService.UpdateCommand(updateCommandDto);
        return NoContent();
    }

    [HttpDelete("{commandId:guid}")]
    public async Task<IActionResult> DeleteCommand(Guid commandId)
    {
        await commandService.DeleteCommand(commandId);
        return NoContent();
    }
}
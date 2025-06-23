using Microsoft.AspNetCore.Mvc;
using RemoteControlUbuntu.Application.Exceptions;
using RemoteControlUbuntu.Application.Services;
using RemoteControlUbuntu.Domain.Dtos;
using RemoteControlUbuntu.Domain.Entities;

namespace RemoteControlUbuntu.API.Controllers;

[ApiController]
[Route("api/command-set")]
public class CommandSetController(ICommandSetService commandSetService) : ControllerBase
{
    [HttpGet("by-user/{userId:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid userId)
    {
        var commandsSet = await commandSetService.GetCommandSetsAsync(userId);
        
        return Ok(commandsSet);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CommandSet commandSet)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await commandSetService.CreateCommandSetAsync(commandSet);
        
        return CreatedAtAction(nameof(Get), new { userId = commandSet.UserId }, commandSet);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CommandSet commandSet)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != commandSet.Id)
        {
            return BadRequest("Id in route does not match Id in body");
        }

        try
        {
            await commandSetService.UpdateCommandSetAsync(commandSet);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await commandSetService.DeleteCommandSetAsync(id);
            return NoContent();
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var commandSet = await commandSetService.GetCommandSetByIdAsync(id);
            return Ok(commandSet);
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
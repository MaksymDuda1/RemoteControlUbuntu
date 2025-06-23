using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Domain.Dtos;

namespace RemoteControlUbuntu.API.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize]
public class CommandsManagementController(ICommandsManagementService commandsManagementService) : ControllerBase
{
    [HttpGet("blacklist")]
    public async Task<ActionResult<List<BlackListCommandDto>>> GetBlackListCommands()
    {
        try
        {
            var commands = await commandsManagementService.GetBlackListCommands();
            return Ok(commands);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving blacklist commands", error = ex.Message });
        }
    }
    
    [HttpPost("blacklist")]
    public async Task<IActionResult> AddCommandToBlackList([FromBody] AddCommandToBlackListDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await commandsManagementService.AddCommandToBlackList(dto);
            return CreatedAtAction(nameof(GetBlackListCommands), new { message = "Command successfully added to blacklist" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while adding command to blacklist", error = ex.Message });
        }
    }
    
    [HttpPut("blacklist/{id:guid}")]
    public async Task<IActionResult> UpdateBlackListCommand(Guid id, [FromBody] BlackListCommandDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await commandsManagementService.UpdateBlackListCommand(id, dto);
            return Ok(new { message = "Blacklist command updated successfully" });
        }
        catch (ArgumentException)
        {
            return NotFound(new { message = "Blacklist command not found" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating blacklist command", error = ex.Message });
        }
    }
    
    [HttpDelete("blacklist/{id:guid}")]
    public async Task<IActionResult> DeleteBlackListCommand(Guid id)
    {
        try
        {
            await commandsManagementService.DeleteBlackListCommand(id);
            return Ok(new { message = "Blacklist command deleted successfully" });
        }
        catch (ArgumentException)
        {
            return NotFound(new { message = "Blacklist command not found" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting blacklist command", error = ex.Message });
        }
    }

    [HttpGet("whitelist")]
    public async Task<ActionResult<List<WhiteListCommandDto>>> GetWhiteListCommands()
    {
        try
        {
            var commands = await commandsManagementService.GetWhiteListCommands();
            return Ok(commands);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving whitelist commands", error = ex.Message });
        }
    }

    [HttpPost("whitelist")]
    public async Task<IActionResult> AddCommandToWhiteList([FromBody] AddCommandToBlackListDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await commandsManagementService.AddCommandToWhiteList(dto);
            return CreatedAtAction(nameof(GetWhiteListCommands), new { message = "Command successfully added to whitelist" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while adding command to whitelist", error = ex.Message });
        }
    }
    
    [HttpPut("whitelist/{id:guid}")]
    public async Task<IActionResult> UpdateWhiteListCommand(Guid id, [FromBody] WhiteListCommandDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await commandsManagementService.UpdateWhiteListCommands(id, dto);
            return Ok(new { message = "Whitelist command updated successfully" });
        }
        catch (ArgumentException)
        {
            return NotFound(new { message = "Whitelist command not found" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating whitelist command", error = ex.Message });
        }
    }
    
    [HttpDelete("whitelist/{id:guid}")]
    public async Task<IActionResult> DeleteWhiteListCommand(Guid id)
    {
        try
        {
            await commandsManagementService.DeleteWhiteListCommand(id);
            return Ok(new { message = "Whitelist command deleted successfully" });
        }
        catch (ArgumentException)
        {
            return NotFound(new { message = "Whitelist command not found" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting whitelist command", error = ex.Message });
        }
    }
}
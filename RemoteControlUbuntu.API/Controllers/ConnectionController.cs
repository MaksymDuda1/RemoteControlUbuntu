using Microsoft.AspNetCore.Mvc;
using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Domain.Abstractions;
using RemoteControlUbuntu.Domain.Dtos;
using RemoteControlUbuntu.Domain.Models;

namespace RemoteControlUbuntu.API.Controllers;

[ApiController]
[Route("api/connection")]
public class ConnectionController(
    IConnectionService connectionService,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ConnectionDto>> GetAll()
    {
        return Ok(await unitOfWork.Connections.GetAllAsync());
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetAllUserConnections(
        [FromRoute] Guid userId,
        [FromQuery] string? name,
        string? host,
        string? username,
        int pageIndex = 1,
        int pageSize = 15)
    {
        var connections = await connectionService.GetAllUserConnections(new ConnectionFilterModel(userId, name, host, username, pageIndex, pageSize));
        
        return Ok(connections);
    }

    [HttpGet("{connectionId:guid}")]
    public async Task<IActionResult> GetConnectionById(Guid connectionId)
    {
        return Ok(await connectionService.GetConnectionById(connectionId));
    }

    [HttpPost]
    public async Task<IActionResult> AddConnection([FromBody] AddConnectionDto connectionDto)
    {
        await connectionService.AddConnection(connectionDto);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateConnection([FromRoute] Guid id,[FromBody] UpdateConnectionDto updateConnectionDto)
    {
        updateConnectionDto.ConnectionId = id;
        
        await connectionService.UpdateConnection(updateConnectionDto);
        return NoContent();
    }

    [HttpDelete("{connectionId:guid}")]
    public async Task<IActionResult> DeleteConnection(Guid connectionId)
    {
        await connectionService.DeleteConnection(connectionId);
        return NoContent();
    }
}
    

using Microsoft.AspNetCore.Mvc;
using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Domain.Abstractions;
using RemoteControlUbuntu.Domain.Dtos;

namespace RemoteControlUbuntu.API.Controllers;

[ApiController]
[Route("api/connection")]
public class ConnectionController(IConnectionService connectionService, IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ConnectionDto>> GetAll()
    {
        return Ok(await unitOfWork.Connections.GetAllAsync());
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetAllUserConnections(Guid userId)
    {
        return Ok(await connectionService.GetAllUserConnections(userId));
    }

    [HttpGet("by-id/{connectionId:guid}")]
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

    [HttpPut]
    public async Task<IActionResult> UpdateConnection([FromBody] UpdateConnectionDto updateConnectionDto)
    {
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
    

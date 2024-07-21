using Microsoft.AspNetCore.Mvc;
using RemoteControlUbuntu.Domain.Abstractions;

namespace RemoteControlUbuntu.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllUsers()
    {
        return Ok(await unitOfWork.Users.GetAllAsync());
    }
}
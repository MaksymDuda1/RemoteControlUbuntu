using Microsoft.AspNetCore.Mvc;
using RemoteControlUbuntu.Infrastructure.Abstractions.Repositories;

namespace RemoteControlUbuntu.Web.Controllers;

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
using Microsoft.AspNetCore.Mvc;

namespace Signicat.Dotnet.Interview.Excercise.Api.Controllers;

[ApiController]
public class AdminController : ControllerBase
{
    /// <summary>
    /// Internal endpoint for listing all users
    /// </summary>
    /// <returns></returns>
    [HttpGet("admin/users")]
    [ProducesResponseType(200)]
    public IActionResult ListUsers()
    {
        return new ObjectResult(null)
        {
            StatusCode = 501
        };
    }
}
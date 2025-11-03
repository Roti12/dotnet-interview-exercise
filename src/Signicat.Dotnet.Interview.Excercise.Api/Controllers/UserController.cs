using Microsoft.AspNetCore.Mvc;
using Signicat.Dotnet.Interview.Excercise.Api.Models;

namespace Signicat.Dotnet.Interview.Excercise.Api.Controllers;

[ApiController]
public class UserController() : ControllerBase
{
    [HttpPost("users")]
    [ProducesResponseType(typeof(User), 200)]
    [ProducesResponseType(400)]
    public IActionResult CreateUser([FromBody] User dto)
    {
        return new ObjectResult(null)
        {
            StatusCode = 501
        };
    }
    
    [HttpPost("users/{userId}/password")]
    [ProducesResponseType(typeof(User), 200)]
    [ProducesResponseType(400)]
    public IActionResult UpdateUserPassword([FromBody] User dto, string userId)
    {
        return new ObjectResult(null)
        {
            StatusCode = 501
        };
    }
    
    [HttpPost("users/authenticate")]
    [ProducesResponseType( 200)]
    [ProducesResponseType(400)]
    public IActionResult Authenticate([FromBody] User dto)
    {
        return new ObjectResult(null)
        {
            StatusCode = 501
        };
    }
}
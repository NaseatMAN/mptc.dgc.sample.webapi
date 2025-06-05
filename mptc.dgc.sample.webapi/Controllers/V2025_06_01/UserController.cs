using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using mptc.dgc.sample.application.DTOs.User;
using mptc.dgc.sample.webapi.Filter;


namespace mptc.dgc.sample.webapi.Controllers.V2025_06_01;

[ServiceFilter(typeof(ApiDeprecateActionFilter))]
[ApiVersion("2025-06-01")]
[Route("users")]
[Produces("application/json")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public IActionResult  Get()
    {
        return Ok("Hello version 2025-06-01");
    }

}
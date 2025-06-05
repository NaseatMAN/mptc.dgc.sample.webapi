using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using mptc.dgc.sample.application.DTOs;
using mptc.dgc.sample.application.DTOs.Error;
using mptc.dgc.sample.application.DTOs.Success;
using mptc.dgc.sample.application.DTOs.User;
using mptc.dgc.sample.application.Interfaces.IUser;

namespace mptc.dgc.sample.webapi.Controllers.V2025_05_01;

[ApiVersion("2025-05-01")]
[Route("users")]
[Produces("application/json")]
[ApiController]
public class UserController(IUserRepository userRepository) : ControllerBase
{
    [HttpGet("page")]
    [ProducesResponseType(typeof(ResponsePagingDto<UserReadDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorDto),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPag([FromQuery] PaginationQueryParams param)
    {
        var user = await userRepository.GetUsersPagedAsync(param);
        return StatusCode(StatusCodes.Status200OK, user);
    }


    [HttpGet("{userId:int}")]
    [ProducesResponseType(typeof(UserReadDto) ,StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetUser(int userId)
    {
            
        var user = await userRepository.GetUserByIdAsync(userId);
        return StatusCode(StatusCodes.Status200OK, user);
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SaveUser([FromBody]UserDto user)
    {
        var savedUser = await userRepository.CreateUserAsync(user);
        return StatusCode(StatusCodes.Status201Created, savedUser);
    }

    [HttpDelete("{userId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        await userRepository.DeleteUserAsync(userId);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
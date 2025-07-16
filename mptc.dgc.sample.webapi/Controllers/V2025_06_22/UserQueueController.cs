
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using mptc.dgc.sample.application.DTOs.User;
using mptc.dgc.sample.application.Interfaces;
using mptc.dgc.sample.application.Services;

namespace mptc.dgc.sample.webapi.Controllers.V2025_06_22
{
    [ApiVersion("2025-06-22")]
    [Route("users/queue")]
    [Produces("application/json")]
    [ApiController]
    public class UserQueueController(ISentQueueMessageService sentQueueMessage,IRedisQueueService redisQueue) : ControllerBase
    {

        [HttpPost("create")]
        public async Task<IActionResult> Login(UserDto user)
        {
            await sentQueueMessage.SendMessageAsync(user);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("redis/create")]
        public async Task<IActionResult> EndQueue(UserDto user)
        {
            await redisQueue.EnqueueAsync(user);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpGet("redis/dequeue")]
        public async Task<IActionResult> DeQueue()
        {
            var count = await redisQueue.GetQueueLengthAsync();
            return StatusCode(StatusCodes.Status200OK,count);
        }
    }
}

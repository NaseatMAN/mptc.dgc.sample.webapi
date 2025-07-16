using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using mptc.dgc.sample.infrastructure.Models;
using Newtonsoft.Json;

namespace mptc.dgc.sample.azurefunction
{
    public class TestQueueFunction
    {
        private readonly ILogger<TestQueueFunction> _logger;
        private readonly SampleContext dbContext;
        public TestQueueFunction(ILogger<TestQueueFunction> logger, SampleContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        [Function("TestQueueFunction")]
        public async Task Run([QueueTrigger("userqueue", Connection = "AzureWebJobsStorage")] string queueMessage, FunctionContext context)
        {
            try
            {
                var userSerialize = JsonConvert.DeserializeObject<UserDto>(queueMessage);
                var logger = context.GetLogger("TestQueueFunction");
                logger.LogInformation($"Received: {queueMessage}");

                if (userSerialize == null)
                {
                    logger.LogError("Deserialized user is null.");
                    return;
                }

                var user = new User
                {
                    CreatedAt = DateTime.Now,
                    Email = userSerialize.Email,
                    Name = userSerialize.Name
                };
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

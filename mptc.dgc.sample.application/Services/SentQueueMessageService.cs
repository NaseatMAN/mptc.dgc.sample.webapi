using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Azure.Storage.Queues;
using mptc.dgc.sample.application.DTOs.User;

namespace mptc.dgc.sample.application.Services
{
    public class SentQueueMessageService(IConfiguration configuration) : ISentQueueMessageService
    {
        private readonly string _connectionString = configuration.GetSection("AzureFunction:AzureWebJobsStorage").Value!;
        private readonly string _queueName = "userqueue";

        public async Task SendMessageAsync<T>(T message)
        {
            try
            {
                var queueClient = new QueueClient(_connectionString, _queueName);
                await queueClient.CreateIfNotExistsAsync();

                var jsonMessage = JsonSerializer.Serialize(message);
                await queueClient.SendMessageAsync(jsonMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

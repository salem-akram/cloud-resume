using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class GetResumeCounter
    {
        private readonly ILogger<GetResumeCounter> _logger;
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public GetResumeCounter(ILogger<GetResumeCounter> logger, CosmosClient cosmosClient)
        {
            _logger = logger;
            _cosmosClient = cosmosClient;
            _container = _cosmosClient.GetContainer("AzureResume", "Counter");
        }

        [Function("GetResumeCounter")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // Retrieve the current counter document from Cosmos DB
            var counterId = "1"; // Replace with your actual document ID
            ItemResponse<CounterData> response = await _container.ReadItemAsync<CounterData>(counterId, new PartitionKey(counterId));
            CounterData counter = response.Resource;

            // Increment the counter
            counter.Count += 1;

            // Update the document in Cosmos DB
            await _container.ReplaceItemAsync(counter, counterId, new PartitionKey(counterId));

            // Serialize the updated counter to JSON
            var jsonResponse = JsonSerializer.Serialize(counter);

            // Create and return the HTTP response
            var responseMessage = req.CreateResponse(HttpStatusCode.OK);
            responseMessage.Headers.Add("Content-Type", "application/json");
            await responseMessage.WriteStringAsync(jsonResponse);

            return responseMessage;
        }
    }

    public class CounterData
    {
        public string id { get; set; }
        public int Count { get; set; }
    }
}

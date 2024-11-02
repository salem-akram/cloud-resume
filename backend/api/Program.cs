using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Cosmos;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication() // Use this for ASP.NET Core integration
    .ConfigureServices(services =>
    {
        // Register CosmosClient as a singleton
        services.AddSingleton<CosmosClient>(serviceProvider =>
        {
            // Use your actual Cosmos DB connection string
            var connectionString = Environment.GetEnvironmentVariable("AzureResumeConnectionString");
            return new CosmosClient(connectionString);
        });
        
        // Add Application Insights telemetry
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

await host.RunAsync(); // Run the host asynchronously

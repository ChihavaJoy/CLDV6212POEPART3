using ABCRetailers.Functions.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureAppConfiguration((context, config) =>
    {
        // Load settings from appsettings.json and environment variables
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
        // Register any DI services here
    })
    .Build();

//Ensure tables exist before the Functions host starts handling requests
await TableInitializer.EnsureTablesExistAsync(host.Services.GetRequiredService<IConfiguration>());

await host.RunAsync();




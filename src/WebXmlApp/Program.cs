using WebXmlApp;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();
await app.ResetDatabaseAsync();

app.Run();

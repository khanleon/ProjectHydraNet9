var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

const string ApiKey = "HYDRA-SECRET-KEY";

app.Use(async (context, next) =>
{
    if (!context.Request.Headers.TryGetValue(
            "X-API-KEY",
            out var extractedApiKey))
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("API Key missing.");
        return;
    }

    if (extractedApiKey != ApiKey)
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Invalid API Key.");
        return;
    }

    await next();
});



app.MapGet("/", () => "Hydra Operations Intelligence API Running");

app.MapGet("/api/operations", async () =>
{
    await Task.Delay(5000);

    var response = new
    {
        Source = "Hydra Operations Intelligence API",
        SystemStatus = "Operational",
        ActiveIncidents = 2,
        ServerLoad = "Normal",
        MissionReadiness = "Green",
        Message = "Operations intelligence scan completed.",
        Timestamp = DateTime.UtcNow
    };

    return Results.Ok(response);
});

app.MapPost("/api/operations/analyze", async (OperationsRequest request) =>
{
    await Task.Delay(5000);

    var response = new
    {
        Source = "Hydra Operations Intelligence API",
        Region = request.Region,
        OperationalScore = 91,
        MissionReadiness = "Green",
        Recommendation = "Systems are stable. Continue standard monitoring.",
        Timestamp = DateTime.UtcNow
    };

    return Results.Ok(response);
});

app.Run();

record OperationsRequest(string Region);
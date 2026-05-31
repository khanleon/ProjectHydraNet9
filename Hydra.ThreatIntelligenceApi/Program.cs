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


app.MapGet("/", () => "Hydra Threat Intelligence API Running");

app.MapGet("/api/threats", async () =>
{
    await Task.Delay(3000);

    var response = new
    {
        Source = "Hydra Threat Intelligence API",
        ThreatLevel = "Elevated",
        UnauthorizedLoginAttempts = 23,
        SuspiciousActivityDetected = true,
        Message = "Threat intelligence scan completed.",
        Timestamp = DateTime.UtcNow
    };

    return Results.Ok(response);
});

app.MapPost("/api/threats/analyze", async (ThreatRequest request) =>
{
    await Task.Delay(3000);

    var response = new
    {
        Source = "Hydra Threat Intelligence API",
        Target = request.Target,
        ThreatScore = 78,
        ThreatLevel = "Elevated",
        Recommendation = "Increase monitoring and require multi-factor authentication.",
        Timestamp = DateTime.UtcNow
    };

    return Results.Ok(response);
});

app.Run();

record ThreatRequest(string Target);
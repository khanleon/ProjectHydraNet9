using Hydra.GatewayApi.Models;

namespace Hydra.GatewayApi.Services;

public class IntelligenceService : IIntelligenceService
{
    
private readonly IHttpClientFactory _httpClientFactory;
private readonly ILogger<IntelligenceService> _logger;

public IntelligenceService(
    IHttpClientFactory httpClientFactory,
    ILogger<IntelligenceService> logger)
{
    _httpClientFactory = httpClientFactory;
    _logger = logger;
}

public async Task<IntelligenceBriefingDto> GetBriefingAsync()
{
    Console.WriteLine("***** HYDRA BRIEFING STARTED *****");

    _logger.LogInformation(
        "Hydra briefing request started at {Time}",
        DateTime.UtcNow);

    var threatClient = _httpClientFactory.CreateClient("ThreatApi");
    var operationsClient = _httpClientFactory.CreateClient("OperationsApi");

    object? threatResult = null;
    object? operationsResult = null;

    var threatTask = GetSafeAsync(
        () => threatClient.GetFromJsonAsync<object>("/api/threats"),
        "Threat Intelligence API");

    var operationsTask = GetSafeAsync(
        () => operationsClient.GetFromJsonAsync<object>("/api/operations"),
        "Operations Intelligence API");

    await Task.WhenAll(threatTask, operationsTask);

    threatResult = await threatTask;
    operationsResult = await operationsTask;

    var message =
        threatResult is null || operationsResult is null
            ? "Partial intelligence returned. One or more services were unavailable."
            : "Threat and operations intelligence gathered asynchronously.";

    _logger.LogInformation(
        "Hydra briefing request completed at {Time}",
        DateTime.UtcNow);

    return new IntelligenceBriefingDto
    {
        Title = "Project Hydra Intelligence Briefing",
        Message = message,
        ThreatIntelligence = threatResult,
        OperationsIntelligence = operationsResult,
        Timestamp = DateTime.UtcNow
    };
}


public async Task<IntelligenceAnalysisDto> AnalyzeAsync(
    IntelligenceRequestDto request)
{
    Console.WriteLine("***** HYDRA ANALYSIS STARTED *****");

    _logger.LogInformation(
        "Hydra analysis request started for Target: {Target}, Region: {Region}",
        request.Target,
        request.Region);

    var threatClient = _httpClientFactory.CreateClient("ThreatApi");
    var operationsClient = _httpClientFactory.CreateClient("OperationsApi");

    var threatTask = PostSafeAsync(
        () => threatClient.PostAsJsonAsync(
            "/api/threats/analyze",
            new { Target = request.Target }),
        "Threat Intelligence API");

    var operationsTask = PostSafeAsync(
        () => operationsClient.PostAsJsonAsync(
            "/api/operations/analyze",
            new { Region = request.Region }),
        "Operations Intelligence API");

    await Task.WhenAll(threatTask, operationsTask);

    var threatResult = await threatTask;
    var operationsResult = await operationsTask;

    var message =
        threatResult is null || operationsResult is null
            ? "Partial analysis returned. One or more services were unavailable."
            : "Threat and operations analysis completed asynchronously.";

    _logger.LogInformation(
        "Hydra analysis request completed at {Time}",
        DateTime.UtcNow);

    return new IntelligenceAnalysisDto
    {
        Title = "Project Hydra Intelligence Analysis",
        Message = message,
        ThreatAnalysis = threatResult,
        OperationsAnalysis = operationsResult,
        Timestamp = DateTime.UtcNow
    };
}

private async Task<object?> PostSafeAsync(
    Func<Task<HttpResponseMessage>> apiCall,
    string serviceName)
{
    try
    {
        var response = await apiCall();

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<object>();
    }
    catch (HttpRequestException ex)
    {
        _logger.LogWarning(
            ex,
            "{ServiceName} is unavailable.",
            serviceName);

        return null;
    }
}

    private async Task<object?> GetSafeAsync(
    Func<Task<object?>> apiCall,
    string serviceName)
{
    try
    {
        return await apiCall();
    }
    catch (HttpRequestException ex)
    {
        _logger.LogWarning(
            ex,
            "{ServiceName} is unavailable.",
            serviceName);

        return null;
    }
}
}
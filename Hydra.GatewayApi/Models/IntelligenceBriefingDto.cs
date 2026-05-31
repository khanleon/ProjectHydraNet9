namespace Hydra.GatewayApi.Models;

public class IntelligenceBriefingDto
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public object? ThreatIntelligence { get; set; }
    public object? OperationsIntelligence { get; set; }
    public DateTime Timestamp { get; set; }
}
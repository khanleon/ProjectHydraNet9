namespace Hydra.GatewayApi.Models;

public class IntelligenceAnalysisDto
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public object? ThreatAnalysis { get; set; }
    public object? OperationsAnalysis { get; set; }
    public DateTime Timestamp { get; set; }
}
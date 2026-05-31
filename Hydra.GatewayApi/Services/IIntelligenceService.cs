using Hydra.GatewayApi.Models;

namespace Hydra.GatewayApi.Services;

public interface IIntelligenceService
{
    Task<IntelligenceBriefingDto> GetBriefingAsync();

    Task<IntelligenceAnalysisDto> AnalyzeAsync(
        IntelligenceRequestDto request);
}
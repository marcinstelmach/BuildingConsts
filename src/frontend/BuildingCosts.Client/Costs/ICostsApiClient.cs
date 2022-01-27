using BuildingCosts.Client.Costs.GetCosts;
using Refit;

namespace BuildingCosts.Client.Costs;

public interface ICostsApiClient
{
    [Get("/costs")]
    Task<ApiResponse<IEnumerable<CostDto>>> GetCostsAsync();
}
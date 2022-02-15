using System.Collections.Generic;
using System.Threading.Tasks;
using BuildingCosts.Client.Services.Costs.GetCosts;
using Refit;

namespace BuildingCosts.Client.Services.Costs;

public interface ICostsApiClient
{
    [Get("/costs")]
    Task<ApiResponse<IEnumerable<CostDto>>> GetCostsAsync();
}
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BuildingCosts.Client.Services.Costs.CreateCost;
using BuildingCosts.Client.Services.Costs.GetCosts;
using Refit;

namespace BuildingCosts.Client.Services.Costs;

public interface ICostsApiClient
{
    [Get("/costs")]
    Task<ApiResponse<IEnumerable<CostDto>>> GetCostsAsync();

    [Post("/costs")]
    Task<HttpResponseMessage> AddCostAsync(CreateCostDto dto);
}
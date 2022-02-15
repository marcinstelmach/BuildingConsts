using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace BuildingCosts.Client.Services.Stages;

public interface IStagesApiClient
{
    [Get("/stages")]
    Task<ApiResponse<IEnumerable<StageDto>>> GetStagesAsync();
}
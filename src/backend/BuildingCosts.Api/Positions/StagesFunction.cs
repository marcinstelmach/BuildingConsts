using System.Collections.Generic;
using System.Threading.Tasks;
using BuildingCosts.Application.Stages.GetStages;
using BuildingCosts.Shared.Application.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace BuildingCosts.Api.Positions;

public class StagesFunction
{
    private readonly IQueryDispatcher _queryDispatcher;

    public StagesFunction(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [FunctionName(nameof(GetStagesAsync))]
    public async Task<IActionResult> GetStagesAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "stages")] HttpRequest request)
    {
        var query = new GetStagesQuery();

        var stages = await _queryDispatcher.DispatchQueryAsync<GetStagesQuery, IEnumerable<StageDto>>(query);
        return new OkObjectResult(stages);
    }
}
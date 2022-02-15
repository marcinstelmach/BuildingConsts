using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildingCosts.Client.Services.Stages;

public interface IStagesService
{
    Task<IEnumerable<StageDto>> GetStagesAsync();
}

public class StagesService : IStagesService
{
    private readonly IStagesApiClient _stagesApiClient;

    public StagesService(IStagesApiClient stagesApiClient)
    {
        _stagesApiClient = stagesApiClient;
    }

    public async Task<IEnumerable<StageDto>> GetStagesAsync()
    {
        var result = await _stagesApiClient.GetStagesAsync();
        if (result.IsSuccessStatusCode && result.Content is not null)
        {
            return result.Content;
        }

        throw new Exception($"Getting stages failed with error: {result.Error?.Content}");
    }
}
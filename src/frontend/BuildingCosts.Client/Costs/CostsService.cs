using BuildingCosts.Client.Costs.GetCosts;

namespace BuildingCosts.Client.Costs;

public interface ICostsService
{
    Task<IEnumerable<CostDto>> GetCostsAsync();
}

public class CostsService : ICostsService
{
    private readonly ICostsApiClient _client;

    public CostsService(ICostsApiClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<CostDto>> GetCostsAsync()
    {
        var result = await _client.GetCostsAsync();
        if (result.IsSuccessStatusCode && result.Content is not null)
        {
            return result.Content;
        }

        throw new Exception($"Getting costs failed with error: {result.Error?.Content}");
    }
}
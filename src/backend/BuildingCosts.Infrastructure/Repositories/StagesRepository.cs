using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingCosts.Domain.ReadModels;
using BuildingCosts.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BuildingCosts.Infrastructure.Repositories;

public class StagesRepository : IStagesRepository
{
    private readonly CostsDbContext _costsDbContext;

    public StagesRepository(CostsDbContext costsDbContext)
    {
        _costsDbContext = costsDbContext;
    }

    public async Task<IEnumerable<StageReadModel>> GetStagesAsync()
    {
        return (await _costsDbContext
                .Costs
                .AsNoTracking()
                .Select(x => new StageReadModel(x.Name))
                .ToArrayAsync())
            .Distinct();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using BuildingCosts.Domain.Entities;
using BuildingCosts.Domain.Repositories;
using Dawn;
using Microsoft.EntityFrameworkCore;

namespace BuildingCosts.Infrastructure.Repositories;

public class CostsRepository : ICostsRepository
{
    private readonly CostsDbContext _costsDbContext;

    public CostsRepository(CostsDbContext costsDbContext)
    {
        _costsDbContext = costsDbContext;
    }

    public async Task<IEnumerable<Cost>> GetCostsAsync()
    {
        return await _costsDbContext.Costs.ToArrayAsync();
    }

    public void AddCost(Cost cost)
    {
        Guard.Argument(cost, nameof(cost)).NotNull();

        _costsDbContext.Costs.Add(cost);
    }
}
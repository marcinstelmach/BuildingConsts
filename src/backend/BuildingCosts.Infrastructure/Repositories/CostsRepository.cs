using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuildingCosts.Domain.Entities;
using BuildingCosts.Domain.Repositories;
using BuildingCosts.Shared.BuildingBlocks;
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
        return await _costsDbContext.Costs.AsNoTrackingWithIdentityResolution().ToArrayAsync();
    }

    public async Task<Cost> GetCostAsync(Guid id)
    {
        return await _costsDbContext.Costs.FindAsync(id);
    }

    public void AddCost(Cost cost)
    {
        Insist.IsNotNull(cost);
        _costsDbContext.Costs.Add(cost);
    }
}
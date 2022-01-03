using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuildingCosts.Domain.Entities;

namespace BuildingCosts.Domain.Repositories;

public interface ICostsRepository
{
    Task<IEnumerable<Cost>> GetCostsAsync();

    Task<Cost> GetCostAsync(Guid id);

    void AddCost(Cost cost);
}
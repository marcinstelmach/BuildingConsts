using System.Collections.Generic;
using System.Threading.Tasks;
using BuildingCosts.Domain.Entities;

namespace BuildingCosts.Domain.Repositories;

public interface ICostsRepository
{
    Task<IEnumerable<Cost>> GetCostsAsync();

    void AddCost(Cost cost);
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingCosts.Domain.ReadModels;
using BuildingCosts.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BuildingCosts.Infrastructure.Repositories;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly CostsDbContext _costsDbContext;

    public CategoriesRepository(CostsDbContext costsDbContext)
    {
        _costsDbContext = costsDbContext;
    }

    public async Task<IEnumerable<CategoryReadModel>> GetCategoriesAsync()
    {
        return (await _costsDbContext
                .Costs
                .AsNoTracking()
                .Select(x => new CategoryReadModel(x.Category.Name))
                .ToArrayAsync())
            .Distinct();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using BuildingCosts.Domain.ReadModels;

namespace BuildingCosts.Domain.Repositories;

public interface ICategoriesRepository
{
    Task<IEnumerable<CategoryReadModel>> GetCategoriesAsync();
}
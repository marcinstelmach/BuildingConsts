using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingCosts.Domain.Repositories;
using BuildingCosts.Shared.Application.Abstract;
using Dawn;

namespace BuildingCosts.Application.Categories.GetCategories;

public class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly ICategoriesRepository _categoriesRepository;

    public GetCategoriesQueryHandler(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<IEnumerable<CategoryDto>> HandleAsync(GetCategoriesQuery query)
    {
        Guard.Argument(query, nameof(query)).NotNull();

        var categories = await _categoriesRepository.GetCategoriesAsync();
        return categories.Select(x => new CategoryDto(x.Name));
    }
}
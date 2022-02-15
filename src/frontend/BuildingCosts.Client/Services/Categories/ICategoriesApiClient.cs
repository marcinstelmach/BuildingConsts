using System.Collections.Generic;
using System.Threading.Tasks;
using BuildingCosts.Client.Services.Categories.GetCategoriesDtos;
using Refit;

namespace BuildingCosts.Client.Services.Categories;

public interface ICategoriesApiClient
{
    [Get("/categories")]
    Task<ApiResponse<IEnumerable<CategoryDto>>> GetCategoriesAsync();
}
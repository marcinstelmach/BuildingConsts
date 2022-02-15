using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuildingCosts.Client.Services.Categories.GetCategoriesDtos;

namespace BuildingCosts.Client.Services.Categories;

public interface ICategoriesService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
}

public class CategoriesService : ICategoriesService
{
    private readonly ICategoriesApiClient _categoriesApiClient;

    public CategoriesService(ICategoriesApiClient categoriesApiClient)
    {
        _categoriesApiClient = categoriesApiClient;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        var response = await _categoriesApiClient.GetCategoriesAsync();
        if (response.IsSuccessStatusCode)
        {
            return response.Content;
        }
        
        throw new Exception($"Getting categories failed with error: {response.Error?.Content}");
    }
}
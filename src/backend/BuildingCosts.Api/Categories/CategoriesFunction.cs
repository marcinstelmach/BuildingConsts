using System.Collections.Generic;
using System.Threading.Tasks;
using BuildingCosts.Application.Categories.GetCategories;
using BuildingCosts.Shared.Application.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace BuildingCosts.Api.Categories;

public class CategoriesFunction
{
    private readonly IQueryDispatcher _queryDispatcher;

    public CategoriesFunction(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [FunctionName(nameof(GetCategoriesAsync))]
    public async Task<IActionResult> GetCategoriesAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "categories")]
        HttpRequest request)
    {
        var query = new GetCategoriesQuery();
        var dtos = await _queryDispatcher.DispatchQueryAsync<GetCategoriesQuery, IEnumerable<CategoryDto>>(query);

        return new OkObjectResult(dtos);
    }
}
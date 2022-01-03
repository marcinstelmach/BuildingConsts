using System.Collections.Generic;
using BuildingCosts.Shared.Application.Abstract;

namespace BuildingCosts.Application.Categories.GetCategories;

public class GetCategoriesQuery : IQuery<IEnumerable<CategoryDto>>
{
}
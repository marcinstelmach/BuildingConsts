using System.Collections.Generic;
using BuildingCosts.Application.Dtos;
using BuildingCosts.Shared.Application;

namespace BuildingCosts.Application.Costs.Queries;

public class GetCostsQuery : IQuery<IEnumerable<CostDto>>
{
}
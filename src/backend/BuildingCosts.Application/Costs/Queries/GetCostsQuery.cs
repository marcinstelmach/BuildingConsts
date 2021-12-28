using System.Collections.Generic;
using BuildingCosts.Application.Costs.Dtos;
using BuildingCosts.Shared.Application.Abstract;

namespace BuildingCosts.Application.Costs.Queries;

public class GetCostsQuery : IQuery<IEnumerable<CostDto>>
{
}
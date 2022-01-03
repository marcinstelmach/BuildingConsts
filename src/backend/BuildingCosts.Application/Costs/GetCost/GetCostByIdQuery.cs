using System;
using BuildingCosts.Shared.Application.Abstract;
using BuildingCosts.Shared.BuildingBlocks.Errors;
using OneOf;

namespace BuildingCosts.Application.Costs.GetCost;

public class GetCostByIdQuery : IQuery<OneOf<DetailedCostDto, Error>>
{
    public GetCostByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
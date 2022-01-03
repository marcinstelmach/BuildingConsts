using System;
using System.Collections.Generic;
using BuildingCosts.Application.Costs.GetCosts;
using BuildingCosts.Shared.Application.Abstract;
using BuildingCosts.Shared.BuildingBlocks;
using OneOf;

namespace BuildingCosts.Application.Costs.CreateCost;

public class CreateCostCommand : ICommand<OneOf<Guid, Error>>
{
    public CreateCostCommand(string name, string description, string stage, string category, IEnumerable<PositionDto> positions)
    {
        Name = name;
        Description = description;
        Stage = stage;
        Category = category;
        Positions = positions;
    }

    public string Name { get; }

    public string Description { get; }

    public string Stage { get; }

    public string Category { get; }

    public IEnumerable<PositionDto> Positions { get; }
}
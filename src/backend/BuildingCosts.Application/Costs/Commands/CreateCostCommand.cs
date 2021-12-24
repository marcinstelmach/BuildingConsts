using System.Collections.Generic;
using BuildingCosts.Application.Dtos;
using BuildingCosts.Shared.Application;

namespace BuildingCosts.Application.Costs.Commands;

public class CreateCostCommand : ICommand
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
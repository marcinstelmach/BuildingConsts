using System;
using System.Collections.Generic;
using BuildingCosts.Shared.Application.Abstract;
using OneOf;
using Error = BuildingCosts.Shared.BuildingBlocks.Errors.Error;

namespace BuildingCosts.Application.Costs.UpdateCost;

public class UpdateCostCommand : ICommand<OneOf<CostSuccessfullyUpdated, Error>>
{
    public UpdateCostCommand(
        Guid id,
        string name,
        string description,
        string stage,
        string category,
        IEnumerable<PositionDto> positions)
    {
        Id = id;
        Name = name;
        Description = description;
        Stage = stage;
        Category = category;
        Positions = positions;
    }

    public Guid Id { get; }

    public string Name { get; }

    public string Description { get; }

    public string Stage { get; }

    public string Category { get; }

    public IEnumerable<PositionDto> Positions { get; }

    public record struct PositionDto(string Name, string Description, decimal GrossPricePerEach, int Count, string Unit, DateTime? PaymentDate);
}
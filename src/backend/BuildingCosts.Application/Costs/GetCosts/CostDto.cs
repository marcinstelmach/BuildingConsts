using System;
using System.Collections.Generic;

namespace BuildingCosts.Application.Costs.GetCosts;

public class CostDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public string Stage { get; init; }

    public string Category { get; init; }

    public DateTime CreationDateTime { get; init; }

    public bool IsPayed { get; init; }

    public decimal GrossPrice { get; init; }

    public IEnumerable<PositionDto> Positions { get; init; }
}
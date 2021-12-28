using System;
using System.Collections.Generic;

namespace BuildingCosts.Application.Costs.Dtos;

public class CostDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Stage { get; set; }

    public string Category { get; set; }

    public DateTime CreationDateTime { get; set; }

    public bool IsPayed { get; set; }

    public decimal GrossPrice { get; set; }

    public IEnumerable<PositionDto> Positions { get; set; }
}
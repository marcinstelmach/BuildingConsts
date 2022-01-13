using System;

namespace BuildingCosts.Application.Costs.GetCosts;

public record struct PositionDto
{
    public string Name { get; init; }

    public string Description { get; init; }

    public decimal GrossPricePerEach { get; init; }

    public int Count { get; init; }

    public string Unit { get; init; }

    public DateTime? PaymentDate { get; init; }
}
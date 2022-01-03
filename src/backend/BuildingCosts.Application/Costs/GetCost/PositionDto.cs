using System;

namespace BuildingCosts.Application.Costs.GetCost;

public record PositionDto
{
    public string Name { get; init; }

    public string Description { get; init; }

    public decimal GrossPricePerEach { get; init; }

    public int Count { get; init; }

    public string Unit { get; init; }

    public DateTime CreationDateTime { get; init; }

    public DateOnly? PaymentDate { get; init; }
}
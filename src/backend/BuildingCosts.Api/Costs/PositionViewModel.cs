using System;

namespace BuildingCosts.Api.Costs;

public class PositionViewModel
{
    public string Name { get; init; }

    public string Description { get; init; }

    public decimal GrossPricePerEach { get; init; }

    public int Count { get; init; }

    public string Unit { get; init; }

    public DateTime? PaymentDateTime { get; init; }
}
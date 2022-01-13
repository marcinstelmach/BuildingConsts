using System;
using System.Collections.Generic;

namespace BuildingCosts.Api.Costs;

public record UpdateCostRequest
{
    public string Name { get; init; }

    public string Description { get; init; }

    public string Stage { get; init; }

    public string Category { get; init; }

    public IEnumerable<PositionViewModel> Positions { get; init; }

    public record struct PositionViewModel
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public decimal GrossPricePerEach { get; init; }

        public int Count { get; init; }

        public string Unit { get; init; }

        public DateTime? PaymentDate { get; init; }
    }
}
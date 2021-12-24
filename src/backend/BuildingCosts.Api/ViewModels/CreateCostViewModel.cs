﻿using System.Collections.Generic;

namespace BuildingCosts.Api.ViewModels;

public class CreateCostViewModel
{
    public string Name { get; init; }

    public string Description { get; init; }

    public string Stage { get; init; }

    public string Category { get; init; }

    public IEnumerable<PositionViewModel> Positions { get; init; }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuildingCosts.Client.Services.Costs.CreateCost;

public class CreateCostDto
{
    [Required(ErrorMessage = "Nazwa jest wymagana")]
    public string Name { get; set; }

    public string Description { get; set; }

    public string Stage { get; set; }

    public string Category { get; set; }

    public List<PositionDto> Positions { get; set; } = new();
}

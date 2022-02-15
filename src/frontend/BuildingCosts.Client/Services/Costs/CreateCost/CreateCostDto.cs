using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BuildingCosts.Client.Services.Costs.CreateCost;

public class CreateCostDto : IValidatableObject
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public string Stage { get; set; }

    [Required]
    public string Category { get; set; }

    public List<PositionDto> Positions { get; init; } = new();
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Positions is not null && !Positions.Any())
        {
            yield return new ValidationResult("Positions require at least one position", new[] { nameof(Positions) });
        }
    }
}

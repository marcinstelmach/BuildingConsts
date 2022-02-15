using System;
using System.ComponentModel.DataAnnotations;

namespace BuildingCosts.Client.Services.Costs.CreateCost;

public class PositionDto
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public decimal? GrossPricePerEach { get; set; }

    [Required]
    public int? Count { get; set; }

    [Required]
    public string Unit { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; } = DateTime.Now;
}
using System;
using System.ComponentModel.DataAnnotations;

namespace BuildingCosts.Application.Dtos;

public class PositionDto
{
    public string Name { get; init; }

    public string Description { get; init; }

    public decimal GrossPricePerEach { get; init; }

    public int Count { get; init; }

    public string Unit { get; init; }

    public DateTime? PaymentDateTime { get; init; }
}
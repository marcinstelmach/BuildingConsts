using System;
using System.Collections.Generic;
using BuildingCosts.Shared.BuildingBlocks;
using Dawn;

namespace BuildingCosts.Domain.ValueObjects;

public class Position : ValueObject<Position>
{
    private Position(string name, string description, decimal grossPricePerEach, int count, string unit, DateTime creationDateTime, DateTime? paymentDateTime)
    {
        Name = name;
        Description = description;
        GrossPricePerEach = grossPricePerEach;
        Count = count;
        Unit = unit;
        CreationDateTime = creationDateTime;
        PaymentDateTime = paymentDateTime;
    }

    private Position()
    {
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public decimal GrossPricePerEach { get; private set; }

    public int Count { get; private set; }

    public string Unit { get; private set; }

    public decimal FinalPrice => Count * GrossPricePerEach;

    public bool IsPayed => PaymentDateTime.HasValue;

    public DateTime CreationDateTime { get; private set; }

    public DateTime? PaymentDateTime { get; private set; }

    public static Position Create(string name, string description, decimal grossPricePerEach, int count, string unit, DateTime creationDateTime, DateTime? paymentDateTime)
    {
        Guard.Argument(name).NotNull().NotWhiteSpace();
        Guard.Argument(description).NotNull().NotWhiteSpace();
        Guard.Argument(grossPricePerEach).GreaterThan(0);
        Guard.Argument(count).GreaterThan(0);
        Guard.Argument(unit).NotNull().NotWhiteSpace();
        Guard.Argument(creationDateTime).LessThan(DateTime.UtcNow);

        if (paymentDateTime.HasValue && paymentDateTime.Value > DateTime.UtcNow)
        {
            throw new ArgumentException("Cannot be in future", nameof(paymentDateTime));
        }

        return new Position(name, description, grossPricePerEach, count, unit, creationDateTime, paymentDateTime);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Name;
        yield return Description;
        yield return GrossPricePerEach;
        yield return Count;
        yield return Unit;
        yield return CreationDateTime;
        yield return FinalPrice;
        yield return IsPayed;
        yield return PaymentDateTime;
    }
}
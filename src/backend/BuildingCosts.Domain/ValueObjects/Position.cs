using System;
using System.Collections.Generic;
using BuildingCosts.Shared.BuildingBlocks;
using Dawn;

namespace BuildingCosts.Domain.ValueObjects;

public class Position : ValueObject<Position>
{
    private Position(string name, string description, decimal grossPricePerEach, int count, string unit, DateTime creationDateTime, DateOnly? paymentDate)
    {
        Name = name;
        Description = description;
        GrossPricePerEach = grossPricePerEach;
        Count = count;
        Unit = unit;
        CreationDateTime = creationDateTime;
        PaymentDate = paymentDate;
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

    public bool IsPayed => PaymentDate.HasValue;

    public DateTime CreationDateTime { get; private set; }

    public DateOnly? PaymentDate { get; private set; }

    public static Position Create(string name, string description, decimal grossPricePerEach, int count, string unit, DateTime creationDateTime, DateOnly? paymentDate)
    {
        Guard.Argument(name).NotNull().NotWhiteSpace();
        Guard.Argument(description).NotNull().NotWhiteSpace();
        Guard.Argument(grossPricePerEach).GreaterThan(0);
        Guard.Argument(count).GreaterThan(0);
        Guard.Argument(unit).NotNull().NotWhiteSpace();
        Guard.Argument(creationDateTime).LessThan(DateTime.UtcNow);

        if (paymentDate.HasValue && paymentDate.Value > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new ArgumentException("Cannot be in future", nameof(paymentDate));
        }

        return new Position(name, description, grossPricePerEach, count, unit, creationDateTime, paymentDate);
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
        yield return PaymentDate;
    }
}
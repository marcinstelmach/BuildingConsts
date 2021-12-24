using System.Collections.Generic;
using BuildingCosts.Shared.BuildingBlocks;
using Dawn;

namespace BuildingCosts.Domain.ValueObjects;

public class Category : ValueObject<Category>
{
    private Category(string name)
    {
        Name = name;
    }

    private Category()
    {
    }

    public string Name { get; private set; }

    public static Category Create(string name)
    {
        Guard.Argument(name).NotNull().NotWhiteSpace();
        return new Category(name);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Name;
    }
}
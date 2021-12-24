using System.Collections.Generic;
using BuildingCosts.Shared.BuildingBlocks;
using Dawn;

namespace BuildingCosts.Domain.ValueObjects;

public class Stage : ValueObject<Stage>
{
    private Stage(string name)
    {
        Name = name;
    }

    private Stage()
    {
    }

    public string Name { get; private set; }

    public static Stage Create(string name)
    {
        Guard.Argument(name).NotNull().NotWhiteSpace();
        return new Stage(name);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Name;
    }
}
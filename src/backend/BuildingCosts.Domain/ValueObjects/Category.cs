using Dawn;

namespace BuildingCosts.Domain.ValueObjects;

public record Category
{
    private Category(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public static Category Create(string name)
    {
        Guard.Argument(name).NotNull().NotWhiteSpace();
        return new Category(name);
    }
}
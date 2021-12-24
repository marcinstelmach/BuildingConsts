using System;
using System.Collections.Generic;
using System.Linq;
using BuildingCosts.Domain.ValueObjects;
using BuildingCosts.Shared.BuildingBlocks;
using Dawn;

namespace BuildingCosts.Domain.Entities;

public class Cost : Entity<Guid>, IAggregateRoot
{
    private readonly List<Position> _positions = new ();

    private Cost(string name, string description, Stage stage, Category category, DateTime creationDateTime, IEnumerable<Position> positions)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Stage = stage;
        Category = category;
        CreationDateTime = creationDateTime;
        _positions.AddRange(positions);
    }

    private Cost()
    {
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public Stage Stage { get; private set; }

    public Category Category { get; private set; }

    public DateTime CreationDateTime { get; private set; }

    public bool IsPayed => _positions.All(x => x.IsPayed);

    public decimal GrossPrice => _positions.Sum(x => x.FinalPrice);

    public IReadOnlyList<Position> Positions => _positions;

    public static Cost Create(string name, string description, Stage stage, Category category, DateTime creationDateTime, ICollection<Position> positions)
    {
        Guard.Argument(name).NotNull().NotWhiteSpace();
        Guard.Argument(description).NotNull().NotWhiteSpace();
        Guard.Argument(description).NotNull().NotWhiteSpace();
        Guard.Argument(stage).NotNull();
        Guard.Argument(category).NotNull();
        Guard.Argument(creationDateTime).LessThan(DateTime.UtcNow);
        Guard.Argument(positions, nameof(positions)).NotNull().NotEmpty();

        return new Cost(name, description, stage, category, creationDateTime, positions);
    }
}

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
        IsDeleted = false;
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

    public bool IsDeleted { get; private set; }

    public bool IsPayed => _positions.All(x => x.IsPayed);

    public decimal GrossPrice => _positions.Sum(x => x.FinalPrice);

    public IReadOnlyList<Position> Positions => _positions;

    public static Cost Create(string name, string description, string stageName, string categoryName, DateTime creationDateTime, ICollection<Position> positions)
    {
        Guard.Argument(name, nameof(name)).NotNull().NotWhiteSpace();
        Guard.Argument(description, nameof(description)).NotNull().NotWhiteSpace();
        Guard.Argument(stageName, nameof(stageName)).NotNull().NotWhiteSpace();
        Guard.Argument(categoryName, nameof(categoryName)).NotNull().NotWhiteSpace();
        Guard.Argument(creationDateTime, nameof(creationDateTime)).LessThan(DateTime.UtcNow);
        Guard.Argument(positions, nameof(positions)).NotNull().NotEmpty();

        var stage = Stage.Create(stageName);
        var category = Category.Create(categoryName);

        return new Cost(name, description, stage, category, creationDateTime, positions);
    }

    public void UpdateName(string name)
    {
        Guard.Argument(name, nameof(name)).NotNull().NotWhiteSpace();
        if (Name != name)
        {
            Name = name;
        }
    }

    public void UpdateDescription(string description)
    {
        Guard.Argument(description, nameof(description)).NotNull().NotWhiteSpace();
        if (Description != description)
        {
            Description = description;
        }
    }

    public void UpdateStage(string stageName)
    {
        Guard.Argument(stageName, nameof(stageName)).NotNull().NotWhiteSpace();
        var newStage = Stage.Create(stageName);
        if (Stage != newStage)
        {
            Stage = newStage;
        }
    }

    public void UpdateCategory(string categoryName)
    {
        Guard.Argument(categoryName, nameof(categoryName)).NotNull().NotWhiteSpace();
        var newCategory = Category.Create(categoryName);
        if (Category != newCategory)
        {
            Category = newCategory;
        }
    }

    public void UpdatePositions(ICollection<Position> positions)
    {
        Guard.Argument(positions, nameof(positions)).NotNull().NotEmpty();

        var positionsForDeletion = _positions.Where(x => !positions.Contains(x)).ToArray();
        var positionsForAdd = positions.Where(x => !_positions.Contains(x)).ToArray();

        foreach (var position in positionsForDeletion)
        {
            _positions.Remove(position);
        }

        _positions.AddRange(positionsForAdd);
    }

    public void DeleteCost()
    {
        IsDeleted = true;
    }
}
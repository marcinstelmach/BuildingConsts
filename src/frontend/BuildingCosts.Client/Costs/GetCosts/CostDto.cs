namespace BuildingCosts.Client.Costs.GetCosts;

public class CostDto
{
    public Guid Id { get; init; }
    
    public string Name { get; init; }

    public string Stage { get; init; }
    
    public string Category { get; init; }

    public DateTimeOffset CreationDateTime { get; init; }

    public bool IsPayed { get; init; }

    public decimal GrossPrice { get; init; }
}
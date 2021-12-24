namespace BuildingCosts.Shared.BuildingBlocks;

public class Clock : IClock
{
    public DateTime GetUtcNow() => DateTime.UtcNow;
}
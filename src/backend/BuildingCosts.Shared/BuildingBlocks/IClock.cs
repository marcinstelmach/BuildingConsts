namespace BuildingCosts.Shared.BuildingBlocks;

public interface IClock
{
    DateTime GetUtcNow();
}
namespace BuildingCosts.Shared.BuildingBlocks;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
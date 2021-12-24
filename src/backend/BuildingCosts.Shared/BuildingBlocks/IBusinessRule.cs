namespace BuildingCosts.Shared.BuildingBlocks;

public interface IBusinessRule<in T>
{
    Result IsValid(T value);
}

public interface IBusinessRule<in T1, in T2>
{
    Result IsValid(T1 value, T2 value2);
}
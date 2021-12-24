namespace BuildingCosts.Shared.BuildingBlocks;

public class Result
{
    private Result(bool succeeded, string error)
    {
        Succeeded = succeeded;
        Error = error;
    }

    public static Result Success => new (true, string.Empty);

    public bool Succeeded { get; }

    public string Error { get; }

    public static Result Failed(string error) => new (false, error);
}
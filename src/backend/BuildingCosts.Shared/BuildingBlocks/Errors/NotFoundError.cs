namespace BuildingCosts.Shared.BuildingBlocks.Errors;

public class NotFoundError : Error
{
    private NotFoundError(string message, int statusCode)
        : base(message, statusCode)
    {
    }

    public static Error Create(string message) => new NotFoundError(message, 404);
}
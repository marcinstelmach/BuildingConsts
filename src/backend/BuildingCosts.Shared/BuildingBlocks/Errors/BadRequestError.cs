namespace BuildingCosts.Shared.BuildingBlocks.Errors;

public class BadRequestError : Error
{
    private BadRequestError(string message, int statusCode)
        : base(message, statusCode)
    {
    }

    public static Error Create(string message) => new BadRequestError(message, 400);
}
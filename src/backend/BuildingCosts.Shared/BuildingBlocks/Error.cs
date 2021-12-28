namespace BuildingCosts.Shared.BuildingBlocks;

public class Error
{
    public Error(string message, int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }

    public string Message { get; }

    public int StatusCode { get; }

    public static Error Create(string message, int statusCode) => new Error(message, statusCode);
}
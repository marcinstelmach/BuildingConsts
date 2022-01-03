namespace BuildingCosts.Shared.BuildingBlocks.Errors;

public abstract class Error
{
    protected Error(string message, int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }

    public string Message { get; }

    public int StatusCode { get; }
}
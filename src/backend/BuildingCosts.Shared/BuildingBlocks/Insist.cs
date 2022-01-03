using System.Runtime.CompilerServices;

namespace BuildingCosts.Shared.BuildingBlocks;

public static class Insist
{
    public static void IsNotNull<T>(T value, [CallerArgumentExpression("value")] string name = "")
    {
        if (value is null)
        {
            throw new ArgumentNullException(name);
        }
    }
}
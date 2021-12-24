using System.Reflection;

namespace BuildingCosts.Application;

public static class ApplicationAssembly
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
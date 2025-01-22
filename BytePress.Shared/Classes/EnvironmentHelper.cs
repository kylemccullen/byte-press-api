using static System.Boolean;
using static System.Environment;

namespace BytePress.Shared.Classes;

public static class EnvironmentHelper
{
    public static string EnvironmentName => GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Localhost";

    public static bool IsDebugging()
    {
        TryParse(GetEnvironmentVariable("DEBUG"), out var isDebugging);
        return isDebugging;
    }

    public static bool AllowAnonymous()
    {
        TryParse(GetEnvironmentVariable("ALLOW_ANONYMOUS"), out var allowAnonymous);
        return allowAnonymous;
    }

    public static bool IsEnvironment(AppEnvironments environment)
    {
        return EnvironmentName == environment.ToString();
    }

    public static bool IsEnvironment(params AppEnvironments[] environments)
    {
        return environments.ToList().Select(e => e.ToString()).Contains(EnvironmentName);
    }
}

public enum AppEnvironments
{
    Localhost
}

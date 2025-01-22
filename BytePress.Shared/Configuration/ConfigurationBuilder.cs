using BytePress.Shared.Classes;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace BytePress.Shared.Configuration;

public static class ConfigurationBuilder
{
    private static string GetAppSettingsPath(string applicationRootPath, string fileName)
    {
        var assemblyDirectory = new DirectoryInfo(applicationRootPath);
        var files = assemblyDirectory.GetFiles(fileName, SearchOption.AllDirectories);
        if (files.Length == 0)
        {
            throw new Exception($"{fileName} not found from {applicationRootPath}");
        }

        return files[0]
            .FullName;
    }

    public static void ConfigureAppConfiguration(this IConfigurationBuilder builder, string applicationRootPath)
    {
        var environmentName = EnvironmentHelper.EnvironmentName;
        var appSettingsPath = GetAppSettingsPath(applicationRootPath, "appsettings.json");
        var appSettingsEnvironmentPath = GetAppSettingsPath(applicationRootPath, $"appsettings.{environmentName}.json");

        builder
            .AddJsonFile(appSettingsPath)
            .AddJsonFile(appSettingsEnvironmentPath)
            .AddEnvironmentVariables();

        if (EnvironmentHelper.IsEnvironment(AppEnvironments.Localhost))
        {
            builder
                .AddUserSecrets(Assembly.GetAssembly(typeof(AppSettings))!, true);
        }
    }
}

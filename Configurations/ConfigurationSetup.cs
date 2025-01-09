namespace DocumentsService.Configurations;

public static class ConfigurationSetup
{
    public static void AddCustomConfiguration(ConfigurationManager configuration, IWebHostEnvironment environment)
    {
        configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddUserSecrets<Program>(optional: true)
            .AddEnvironmentVariables();
    }
}
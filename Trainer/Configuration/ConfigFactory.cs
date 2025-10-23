namespace Trainer.Configuration;

/// <summary>
/// Factory to create configurations from CLI arguments
/// </summary>
public static class ConfigFactory
{
    public static DataSourceConfig FromCliOptions(Options options)
    {
        bool strictMode = !options.NoStrict;
        
        return new DataSourceConfig
        {
            FilePath = options.DataPath,
            FeatureName = options.FeatureName,
            TargetName = options.TargetName,
            StrictMode = strictMode,
        };
    }
}

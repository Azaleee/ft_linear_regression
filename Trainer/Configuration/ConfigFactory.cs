namespace Trainer.Configuration;

/// <summary>
/// Factory to create configurations from CLI arguments
/// </summary>
public static class ConfigFactory
{
    /// <summary>
    /// Creates DataSourceConfig from CLI options
    /// </summary>
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

    /// <summary>
    /// Creates TrainingConfig from CLI Options
    /// </summary>
    public static TrainingConfig CreateTrainingConfig(Options options)
    {
        return new TrainingConfig
        {
            LearningRate = options.LearningRate,
            Iterations = options.Iterations,
            DisplayEvery = options.DisplayEvery,
            Verbose = options.Verbose
        };
    }

    /// <summary>
    /// Creates GraphConfig from CLI options
    /// Uses feature and target names for labels
    /// </summary>
    public static GraphConfig CreateGraphConfig(Options options)
    {
        return new GraphConfig
        {
            Title = $"{options.FeatureName} vs {options.TargetName} with Regression Line",
            XLabel = FormatLabel(options.FeatureName),
            YLabel = FormatLabel(options.TargetName),
            Width = 800,
            Height = 600,
            MarkerSize = 10f,
            LineWidth = 2f
        };
    }
    
    private static string FormatLabel(string columnName)
    {
        // Capitalize first letter
        if (string.IsNullOrEmpty(columnName))
            return columnName;
        
        return char.ToUpper(columnName[0]) + columnName.Substring(1);
    }
}

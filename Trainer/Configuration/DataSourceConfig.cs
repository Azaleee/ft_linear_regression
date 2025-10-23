namespace Trainer.Configuration;

/// <summary>
/// Configuration for the data source
/// </summary>
public class DataSourceConfig
{
    /// <summary>
    /// Path to the CSV file
    /// </summary>
    public string FilePath { get; set; } = "data.csv";
    
    /// <summary>
    /// Name of the feature column (X)
    /// </summary>
    public string FeatureName { get; set; } = "km";
    
    /// <summary>
    /// Name of the target column (Y)
    /// </summary>
    public string TargetName { get; set; } = "price";
    
    /// <summary>
    /// Strict mode: fails on any error
    /// </summary>
    public bool StrictMode { get; set; } = true;
}

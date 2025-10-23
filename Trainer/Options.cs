using CommandLine;

namespace Trainer;

/// <summary>
/// Command-line arguments for the trainer
/// </summary>
public class Options
{
    // === Data Source ===
    
    [Option('d', "data", Required = false, Default = "data.csv", HelpText = "Path to CSV file")]
    public string DataPath { get; set; } = "data.csv";
    
    [Option('f', "feature", Required = false, Default = "km", HelpText = "Name of feature column (X)")]
    public string FeatureName { get; set; } = "km";
    
    [Option('t', "target", Required = false, Default = "price", HelpText = "Name of target column (Y)")]
    public string TargetName { get; set; } = "price";
    
    [Option("no-strict", Required = false, Default = false, HelpText = "Non-strict mode: continue on parsing errors")]
    public bool NoStrict { get; set; } = false;
}

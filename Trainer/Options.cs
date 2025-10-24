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

    // === Training ===
    
    [Option('l', "learning-rate", Required = false, Default = 0.01,
            HelpText = "Learning rate for gradient descent")]
    public double LearningRate { get; set; } = 0.01;
    
    [Option('i', "iterations", Required = false, Default = 1000,
            HelpText = "Number of training iterations")]
    public int Iterations { get; set; } = 1000;
    
    [Option("display-every", Required = false, Default = 100,
            HelpText = "Display progress every N iterations")]
    public int DisplayEvery { get; set; } = 100;
    
    [Option('v', "verbose", Required = false, Default = false,
            HelpText = "Show verbose output during training")]
    public bool Verbose { get; set; } = false;

    // === Output ===
    
    [Option('m', "model", Required = false, Default = "model.txt",
            HelpText = "Path to save the trained model")]
    public string ModelPath { get; set; } = "model.txt";
    
    [Option('p', "plot", Required = false, Default = false,
            HelpText = "Generate visualization plot")]
    public bool GeneratePlot { get; set; } = false;
    
    [Option('o', "output-plot", Required = false, Default = "regression_plot.png",
            HelpText = "Path to save the plot image")]
    public string PlotPath { get; set; } = "regression_plot.png";

    // === Data Cleaning ===

    [Option("clean", Required = false, Default = false,
            HelpText = "Remove outliers using IQR method")]
    public bool CleanData { get; set; } = false;

    [Option("iqr-multiplier", Required = false, Default = 1.5,
            HelpText = "IQR multiplier for outlier detection (1.5=standard, 3.0=extreme)")]
    public double IqrMultiplier { get; set; } = 1.5;

    [Option("clean-feature", Required = false, Default = true,
            HelpText = "Remove outliers based on feature values")]
    public bool CleanFeature { get; set; } = true;

    [Option("clean-target", Required = false, Default = true,
            HelpText = "Remove outliers based on target values")]
    public bool CleanTarget { get; set; } = true;
}

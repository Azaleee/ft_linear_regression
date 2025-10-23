namespace Trainer.Configuration;

/// <summary>
/// Configuration for the training process
/// </summary>
public class TrainingConfig
{
    /// <summary>
    /// Learning rate for gradient descent
    /// </summary>
    public double LearningRate { get; set; } = 0.01;
    
    /// <summary>
    /// Number of training iterations
    /// </summary>
    public int Iterations { get; set; } = 1000;
    
    /// <summary>
    /// Display progress every N iterations
    /// </summary>
    public int DisplayEvery { get; set; } = 100;
    
    /// <summary>
    /// Show verbose output (cost at each display step)
    /// </summary>
    public bool Verbose { get; set; } = false;
}

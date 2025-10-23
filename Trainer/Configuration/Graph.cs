namespace Trainer.Configuration;

/// <summary>
/// Configuration for graph generation
/// </summary>
public class GraphConfig
{
    /// <summary>
    /// Title of the graph
    /// </summary>
    public string Title { get; set; } = "Feature vs Target with Regression Line";
    
    /// <summary>
    /// Label for X axis (feature name)
    /// </summary>
    public string XLabel { get; set; } = "Feature";
    
    /// <summary>
    /// Label for Y axis (target name)
    /// </summary>
    public string YLabel { get; set; } = "Target";
    
    /// <summary>
    /// Width of the image in pixels
    /// </summary>
    public int Width { get; set; } = 800;
    
    /// <summary>
    /// Height of the image in pixels
    /// </summary>
    public int Height { get; set; } = 600;
    
    /// <summary>
    /// Size of data point markers
    /// </summary>
    public float MarkerSize { get; set; } = 10f;
    
    /// <summary>
    /// Width of the regression line
    /// </summary>
    public float LineWidth { get; set; } = 2f;
}

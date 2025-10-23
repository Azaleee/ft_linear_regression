using ScottPlot;
using Trainer.Models;
using Trainer.Configuration;

namespace Trainer.Services;

/// <summary>
/// Generates visualization plots for regression results
/// </summary>
public static class Graph
{
    /// <summary>
    /// Generates a plot with data points and regression line
    /// </summary>
    /// <param name="data">Original (non-normalized) data</param>
    /// <param name="theta0">Intercept parameter</param>
    /// <param name="theta1">Slope parameter</param>
    /// <param name="config">Graph configuration</param>
    /// <param name="outputPath">Path to save the plot</param>
    /// <param name="userFeature">Optional user input to highlight on the plot</param>
    public static void Generate(List<Sample> data, double theta0, double theta1, 
                               GraphConfig config, string outputPath, 
                               double? userFeature = null)
    {
        var plt = new Plot();
        
        double[] features = data.Select(s => s.Feature).ToArray();
        double[] targets = data.Select(s => s.Target).ToArray();
        
        var scatter = plt.Add.Scatter(features, targets);
        scatter.LegendText = "Data Points";
        scatter.MarkerSize = config.MarkerSize;
        scatter.MarkerShape = MarkerShape.FilledCircle;
        scatter.Color = Colors.Blue;
        scatter.LineWidth = 0;
        
        double minFeature = 0;
        double maxFeature = features.Max() * 1.1;
        double[] lineX = { minFeature, maxFeature };
        double[] lineY = { 
            theta0 + theta1 * minFeature,
            theta0 + theta1 * maxFeature
        };
        
        var line = plt.Add.Line(lineX[0], lineY[0], lineX[1], lineY[1]);
        line.LegendText = "Regression Line";
        line.LineWidth = config.LineWidth;
        line.Color = Colors.Red;
        
        if (userFeature.HasValue)
        {
            double userTarget = theta0 + theta1 * userFeature.Value;
            var userPoint = plt.Add.Marker(userFeature.Value, userTarget);
            userPoint.MarkerSize = config.MarkerSize * 1.5f;
            userPoint.MarkerShape = MarkerShape.FilledCircle;
            userPoint.Color = Colors.Green;
            userPoint.LegendText = "User Input";
        }
        
        plt.Title(config.Title);
        plt.XLabel(config.XLabel);
        plt.YLabel(config.YLabel);
        plt.ShowLegend(Alignment.UpperRight);
        
        plt.SavePng(outputPath, config.Width, config.Height);
        Console.WriteLine($"Graph saved: {outputPath}");
    }
}

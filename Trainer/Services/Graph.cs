using ScottPlot;
using Trainer.Models;

namespace Trainer.Services;

public static class Graph
{
    public static void PlotResults<T>(List<T> originalData, double theta0Original, double theta1Original, double? userFeatureData = null) where T : ITrainableData
    {
        var plt = new Plot();
        
        double[] feature = originalData.Select(c => c.GetFeature()).ToArray();
        double[] target = originalData.Select(c => c.GetTarget()).ToArray();
        
        var scatter = plt.Add.Scatter(feature, target);
        scatter.LegendText = "Data Points";
        scatter.MarkerSize = 10;
        scatter.MarkerShape = MarkerShape.FilledCircle;
        scatter.Color = Colors.Blue;
        scatter.LineWidth = 0;
        
        double minFeature = 0;
        double maxTarget = feature.Max() * 1.1;
        double[] lineX = { minFeature, maxTarget };
        double[] lineY = { 
            theta0Original + theta1Original * minFeature,
            theta0Original + theta1Original * maxTarget
        };
        
        var line = plt.Add.Line(lineX[0], lineY[0], lineX[1], lineY[1]);
        line.LegendText = "Regression Line";
        line.LineWidth = 2;
        line.Color = Colors.Red;
        
        if (userFeatureData.HasValue)
        {
            double userTarget = theta0Original + theta1Original * userFeatureData.Value;
            var userPoint = plt.Add.Marker(userFeatureData.Value, userTarget);
            userPoint.MarkerSize = 15;
            userPoint.MarkerShape = MarkerShape.FilledCircle;
            userPoint.Color = Colors.Green;
            userPoint.LegendText = "User Input";
        }
        
        plt.Title("Mileage vs Price with Regression Line");
        plt.XLabel("Mileage (km)");
        plt.YLabel("Price (€)");
        plt.ShowLegend(Alignment.UpperRight);
        
        plt.SavePng("../regression_plot.png", 800, 600);
        Console.WriteLine("\nGenerated graph : regression_plot.png");
    }
}


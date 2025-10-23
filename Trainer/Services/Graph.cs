using ScottPlot;

namespace Trainer.Services;

public class Graph
{

    public static void PlotResults(List<CarData> originalData, double theta0Original, double theta1Original, double? userKm = null)
    {
        var plt = new Plot();
        
        double[] kms = originalData.Select(c => c.km).ToArray();
        double[] prices = originalData.Select(c => c.price).ToArray();
        
        var scatter = plt.Add.Scatter(kms, prices);
        scatter.LegendText = "Data Points";
        scatter.MarkerSize = 10;
        scatter.MarkerShape = MarkerShape.FilledCircle;
        scatter.Color = Colors.Blue;
        scatter.LineWidth = 0;
        
        double minKm = 0;
        double maxKm = kms.Max() * 1.1;
        double[] lineX = { minKm, maxKm };
        double[] lineY = { 
            theta0Original + theta1Original * minKm,
            theta0Original + theta1Original * maxKm
        };
        
        var line = plt.Add.Line(lineX[0], lineY[0], lineX[1], lineY[1]);
        line.LegendText = "Regression Line";
        line.LineWidth = 2;
        line.Color = Colors.Red;
        
        if (userKm.HasValue)
        {
            double userPrice = theta0Original + theta1Original * userKm.Value;
            var userPoint = plt.Add.Marker(userKm.Value, userPrice);
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


using CommandLine;
using Trainer.Configuration;
using Trainer.Models;
using Trainer.Services;
using Trainer.Utils;

namespace Trainer;

class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(Run)
            .WithNotParsed(HandleErrors);
    }

    private static void Run(Options options)
    {
        Console.WriteLine("=== Linear Regression Trainer ===\n");

        var dataConfig = ConfigFactory.FromCliOptions(options);
        var trainingConfig = ConfigFactory.CreateTrainingConfig(options);
        var graphConfig = ConfigFactory.CreateGraphConfig(options);

        var data = new List<Sample>();
        try
        {
            data = DataLoader.LoadFromCsv(dataConfig);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            Environment.Exit(1);
            return;
        }

        Console.WriteLine($"{data.Count} items loaded\n");
        Console.WriteLine($"Original data: {string.Join(", ", data.Take(5).Select(d => $"({d.Feature}, {d.Target})"))} ...\n");

        if (options.CleanData)
        {
            int removed = DataCleaner.RemoveOutliers(
                data,
                removeFeatureOutliers: options.CleanFeature,
                removeTargetOutliers: options.CleanTarget,
                multiplier: options.IqrMultiplier
            );

            if (removed > 0)
            {
                double percentage = (removed * 100.0) / (data.Count + removed);
                Console.WriteLine($"Data cleaned: removed {removed} outliers ({percentage:F1}%)\n");
            }
            else
            {
                Console.WriteLine("Data cleaned: no outliers detected\n");
            }

            if (data.Count == 0)
            {
                Console.WriteLine("Error: All data was removed as outliers!");
                Environment.Exit(1);
                return;
            }
        }

        var normalizer = new DataNormalizer();
        var normalizedData = new List<Sample>();
        try
        {
            normalizedData = normalizer.Normalize(data);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
        Console.WriteLine("Normalized data\n");

        Console.WriteLine("Training in progress ...\n");
        var trainer = new LinearRegressionTrainer(trainingConfig);
        trainer.Train(normalizedData);

        var (theta0, theta1) = normalizer.Denormalize(trainer.Theta0, trainer.Theta1);

        Console.WriteLine($"\nTraining done. Model parameters:");
        Console.WriteLine($"θ0 = {theta0:F6}");
        Console.WriteLine($"θ1 = {theta1:F6}\n");

        ModelSaver.Save(options.ModelPath, theta0, theta1);
        Console.WriteLine($"Model saved to {Path.GetFullPath(options.ModelPath)}\n");

        if (options.GeneratePlot)
        {
            Graph.Generate(data, theta0, theta1, graphConfig, options.PlotPath);
            Console.WriteLine($"Plot saved to {Path.GetFullPath(options.PlotPath)}\n");
        }
    }

    private static void HandleErrors(IEnumerable<Error> enumerable)
    {
        Console.WriteLine("Invalid arguments. Use --help for usage information.");
        Environment.Exit(1);
    }
}

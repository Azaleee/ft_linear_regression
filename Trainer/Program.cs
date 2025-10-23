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

        var config = ConfigFactory.FromCliOptions(options);

        var data = new List<Sample>();
        try
        {
            data = DataLoader.LoadFromCsv(config);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            Environment.Exit(1);
            return;
        }

        Console.WriteLine($"{data.Count} items loaded\n");
        Console.WriteLine($"Original data: {string.Join(", ", data.Take(5).Select(d => $"({d.Feature}, {d.Target})"))} ...\n");
    }

    private static void HandleErrors(IEnumerable<Error> enumerable)
    {
        Console.WriteLine("Invalid arguments. Use --help for usage information.");
        Environment.Exit(1);
    }
}


// var normalizer = new DataNormalizer<HouseData>();
// try
// {
//     normalizer.Normalize(data);
// }
// catch (Exception e)
// {
//     Console.WriteLine($"Error: {e.Message}");
// }
// finally
// {
//     Console.WriteLine("Normalized data\n");
// }
//
// Console.WriteLine("Training in progress ...\n");
// var trainer = new LinearRegressionTrainer<HouseData>(learningRate: 0.1, iterations: 1000000);
// trainer.Train(data);
//
// var (theta0, theta1) = normalizer.Denormalize(trainer.Theta0, trainer.Theta1);
//
//
// Graph.PlotResults(originalData, theta0, theta1, userFeatureData: 280);
//
// Console.WriteLine("\nTraining done\n");
//
// ModelSaver.Save("../model.txt", theta0, theta1);

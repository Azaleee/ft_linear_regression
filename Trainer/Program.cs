using Trainer.Services;
using Trainer.Utils;

Console.WriteLine("=== Linear Regression Trainer ===\n");

var data = DataLoader.LoadFromCsv("../data.csv");
Console.WriteLine($"{data.Count} cars loaded\n");

var normalizer = new DataNormalizer();
try
{
    normalizer.Normalize(data);
}
catch (Exception e)
{
    Console.WriteLine($"Error: {e.Message}");
}
finally
{
    Console.WriteLine("Normalized data\n");
}

Console.WriteLine("Training in progress ...\n");
var trainer = new LinearRegressionTrainer(learningRate: 0.01, iterations: 100000);
trainer.Train(data);

var (theta0, theta1) = normalizer.Denormalize(trainer.Theta0, trainer.Theta1);


var originalData = DataLoader.LoadFromCsv("../data.csv");
Graph.PlotResults(originalData, theta0, theta1, userKm: 150000);

Console.WriteLine("\nTraining done\n");

ModelSaver.Save("../model.txt", theta0, theta1);

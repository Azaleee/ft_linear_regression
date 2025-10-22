namespace Trainer.Utils;

public class ModelSaver
{
    public static void Save(string filePath, double theta0, double theta1)
    {
        File.WriteAllText(filePath, $"{theta0}\n{theta1}");
        Console.WriteLine($"Saved template: {filePath}");
        Console.WriteLine($"θ0 = {theta0}");
        Console.WriteLine($"θ1 = {theta1}");
    }
}

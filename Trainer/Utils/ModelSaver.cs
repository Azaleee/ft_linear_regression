using System.Text;
using System.Text.Json;

namespace Trainer.Utils;

public class ModelSaver
{
    public readonly record struct LinRegModel(double Theta0, double Theta1);

    private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
            AllowTrailingCommas = false
        };

    public static void Save(string filePath, LinRegModel model)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("Model path cannot be null or empty.", nameof(filePath));

        var json = JsonSerializer.Serialize(model);
        File.WriteAllText(filePath, json, new UTF8Encoding(false));
    }

    public static LinRegModel Load(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Model file not found: {filePath}");

        var json = File.ReadAllText(filePath, Encoding.UTF8).Trim();

        if (string.IsNullOrWhiteSpace(json))
            throw new InvalidDataException("Model file is empty.");

        try
        {
            var model = JsonSerializer.Deserialize<LinRegModel>(json, Options);

            if (double.IsNaN(model.Theta0) || double.IsNaN(model.Theta1))
                throw new InvalidDataException("Model contains NaN values.");

            return model;
        }
        catch (JsonException ex)
        {
            throw new InvalidDataException($"Failed to parse model file as JSON: {ex.Message}", ex);
        }
    }
}

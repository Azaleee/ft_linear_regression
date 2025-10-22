namespace LinearRegression;

/// <summary>
/// Helper class to load data from CSV files
/// </summary>
public class DataLoader
{
    /// <summary>
    /// Load data from a CSV file with two columns
    /// </summary>
    /// <param name="filePath">Path to the CSV file</param>
    /// <returns>Tuple of (X values, Y values)</returns>
    public static (double[] X, double[] Y) LoadFromCsv(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }

        var xList = new List<double>();
        var yList = new List<double>();

        var lines = File.ReadAllLines(filePath);
        
        // Skip header line
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');
            if (parts.Length >= 2)
            {
                if (double.TryParse(parts[0], out double x) && double.TryParse(parts[1], out double y))
                {
                    xList.Add(x);
                    yList.Add(y);
                }
            }
        }

        return (xList.ToArray(), yList.ToArray());
    }

    /// <summary>
    /// Save trained model parameters to a file
    /// </summary>
    /// <param name="filePath">Path to save the model</param>
    /// <param name="theta0">Intercept</param>
    /// <param name="theta1">Slope</param>
    public static void SaveModel(string filePath, double theta0, double theta1)
    {
        File.WriteAllText(filePath, $"{theta0},{theta1}");
    }

    /// <summary>
    /// Load trained model parameters from a file
    /// </summary>
    /// <param name="filePath">Path to the model file</param>
    /// <returns>Tuple of (theta0, theta1)</returns>
    public static (double Theta0, double Theta1) LoadModel(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Model file not found: {filePath}");
        }

        var content = File.ReadAllText(filePath);
        var parts = content.Split(',');
        
        if (parts.Length >= 2 && 
            double.TryParse(parts[0], out double theta0) && 
            double.TryParse(parts[1], out double theta1))
        {
            return (theta0, theta1);
        }

        throw new InvalidDataException("Invalid model file format");
    }
}

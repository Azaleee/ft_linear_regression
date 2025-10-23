using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Trainer.Configuration;
using Trainer.Models;

namespace Trainer.Services;

public static class DataLoader
{
    public static List<Sample> LoadFromCsv(DataSourceConfig config)
    {

        if (string.IsNullOrWhiteSpace(config.FilePath))
            throw new ArgumentException("CSV path is empty.");

        var fullPath = Path.GetFullPath(config.FilePath);
        IsFileValid(fullPath);

        var (cfg, culture) = CsvSetup(fullPath);

        using var reader = new StreamReader(fullPath);
        using var csv = new CsvReader(reader, cfg);

        csv.Read();
        csv.ReadHeader();

        var headers = csv.HeaderRecord!.Select(h => h.ToLowerInvariant()).ToArray();
        
        var (xi, yi) = GetColumnIndices(headers, config.FeatureName, config.TargetName);

        var records = new List<Sample>();

        while (csv.Read())
        {
            try
            {
                var x = csv.GetField<double>(xi);
                var y = csv.GetField<double>(yi);
                records.Add(new Sample { Feature = x, Target = y });
            }
            catch (Exception ex)
            {
                var line = csv.Context.Parser!.RawRow;
                var record = csv.Parser.RawRecord?.Trim() ?? "(empty)";
                var msg = $"Error parsing line {line}: '{record}' ({ex.Message})";

                if (config.StrictMode)
                    throw new InvalidDataException(msg, ex);
                else
                    Console.WriteLine("Warning: " + msg);
            }
        }
        return records;
    }
    
    private static void IsFileValid(string fullPath)
    {
        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"CSV file not found: {fullPath}");

        var fileInfo = new FileInfo(fullPath);
        if (fileInfo.Length == 0)
            throw new InvalidDataException("CSV file is empty.");
        if (fileInfo.Length > 20 * 1024 * 1024)
            throw new InvalidDataException("CSV file is too large.");
    }

    private static (CsvConfiguration, CultureInfo) CsvSetup(string fullPath)
    {
        var first = File.ReadLines(fullPath).First();
        var delim = first.Contains(';') ? ";" : (first.Contains('\t') ? "\t" : ",");

        var culture = delim == ";" ? new CultureInfo("fr-FR") : CultureInfo.InvariantCulture;
        
        var cfg = new CsvConfiguration(culture)
        {
            Delimiter = delim,
            HasHeaderRecord = true,
            TrimOptions = TrimOptions.Trim,
            IgnoreBlankLines = true,
            DetectColumnCountChanges = true,
        };

        return (cfg, culture);
    }

    private static (int, int) GetColumnIndices(string[] headers, string xName, string yName)
    {
        int xi = Array.FindIndex(headers, h => h == xName.ToLowerInvariant());
        int yi = Array.FindIndex(headers, h => h == yName.ToLowerInvariant());
        if (xi < 0 || yi < 0)
            throw new InvalidDataException($"Columns not found. Available: {string.Join(", ", headers)}");
        return (xi, yi);
    }
}

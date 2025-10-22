using System.Globalization;
using CsvHelper;

namespace Trainer.Services;

public class DataLoader
{
    public static List<CarData> LoadFromCsv(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("CSV path is empty.");

        var fullPath = Path.GetFullPath(filePath);
        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"CSV file not found: {fullPath}");

        var fileInfo = new FileInfo(fullPath);
        if (fileInfo.Length == 0)
            throw new InvalidDataException("CSV file is empty.");
        if (fileInfo.Length > 20 * 1024 * 1024)
            throw new InvalidDataException("CSV file is too large.");

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<CarData>();
        return records.ToList();
    }
}

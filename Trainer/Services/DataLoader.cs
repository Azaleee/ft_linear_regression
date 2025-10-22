using System.Globalization;
using CsvHelper;

namespace Trainer.Services;

public class DataLoader
{
    public static List<CarData> LoadFromCsv(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<CarData>();
        return records.ToList();
    }
}

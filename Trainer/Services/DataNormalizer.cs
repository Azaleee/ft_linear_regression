namespace Trainer.Services;

public class DataNormalizer
{
    public double MinKm { get; private set; }
    public double MaxKm { get; private set; }
    public double MinPrice { get; private set; }
    public double MaxPrice { get; private set; }

    public void Normalize(List<CarData> data)
    {
        MinKm = data.Min(c => c.km);
        MaxKm = data.Max(c => c.km);
        MinPrice = data.Min(c => c.price);
        MaxPrice = data.Max(c => c.price);

        var dx = MaxKm - MinKm;  if (dx == 0) throw new InvalidOperationException("All km are equal.");
        var dy = MaxPrice - MinPrice; if (dy == 0) throw new InvalidOperationException("All prices are equal.");

        foreach (var car in data)
        {
            car.km = (car.km - MinKm) / (MaxKm - MinKm);
            car.price = (car.price - MinPrice) / (MaxPrice - MinPrice);
        }
    }

    public (double theta0, double theta1) Denormalize(double theta0Norm, double theta1Norm)
    {
        double theta1Original = theta1Norm * (MaxPrice - MinPrice) / (MaxKm - MinKm);
        double theta0Original = MinPrice + theta0Norm * (MaxPrice - MinPrice) - theta1Original * MinKm;
        return (theta0Original, theta1Original);
    }
}

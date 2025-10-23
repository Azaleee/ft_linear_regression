using Trainer.Models;

namespace Trainer.Services;

public class DataNormalizer<T> where T : ITrainableData
{
    public double MinFeature { get; private set; }
    public double MaxFeature { get; private set; }
    public double MinTarget { get; private set; }
    public double MaxTarget { get; private set; }

    public void Normalize(List<T> data)
    {
        MinFeature = data.Min(c => c.GetFeature());
        MaxFeature = data.Max(c => c.GetFeature());
        MinTarget = data.Min(c => c.GetTarget());
        MaxTarget = data.Max(c => c.GetTarget());


        foreach (var item in data)
        {
            double normFeature = (item.GetFeature() - MinFeature) / (MaxFeature - MinFeature);
            double normTarget = (item.GetTarget() - MinTarget) / (MaxTarget - MinTarget);
            
            item.SetFeature(normFeature);
            item.SetTarget(normTarget);
        }
    }

    public (double theta0, double theta1) Denormalize(double theta0Norm, double theta1Norm)
    {
        double theta1Original = theta1Norm * (MaxTarget - MinTarget) / (MaxFeature - MinFeature);
        double theta0Original = MinTarget + theta0Norm * (MaxTarget - MinTarget) - theta1Original * MinFeature;
        return (theta0Original, theta1Original);
    }
}
